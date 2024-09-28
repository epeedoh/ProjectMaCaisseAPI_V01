using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectMaCaisseAPI_V01.IRepository;
using ProjectMaCaisseAPI_V01.Models;
using ProjectMaCaisseAPI_V01.Models.SignUp;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectMaCaisseAPI_V01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly UserManager<Data.User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<Data.User> userManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IUserRepository repo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _repo = repo;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;

            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }


        //// GET: api/<UserController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/<UserController>
        [HttpGet("GetAllUser")]
        public UserResponse GetAllUser()
        {
            var UserResponse = new UserResponse();
            var user = _repo.GetAllUsers();
            UserResponse.users = _mapper.Map<List<UserDto>>(user);
            UserResponse.Statut = (int)HttpStatusCode.OK;
            UserResponse.Message = "Effectuer avec succes";

            return UserResponse;
        }


        // GET api/<UserController>/5
        [HttpPost("GetUsersById")]
        public UserResponse GetUsersById(int idUser)
        {
            var UserResponse = new UserResponse();



            var users = _repo.GetUsersById(idUser);
            UserResponse.users = _mapper.Map<List<UserDto>>(users);
            UserResponse.Message = "Effectuer avec succes";
            UserResponse.Statut = (int)HttpStatusCode.OK;

            return UserResponse;
        }


        private string SaveImageAndGetPath(IFormFile imageFile)
        {
            if(imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Le fichier image est evide.");
            }

            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if(!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Générer un nom de fichier unique pour éviter les conflits
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return "/images/" + uniqueFileName; // Retourne le chemin relatif du fichier sauvegardé

        }

        // POST api/<UserController>
        //[HttpPost("CreateUser")]
        //public async Task<UserResponse> CreateUser([FromBody]Models.User? anUser, Models.User registerUser)
        //{


        //    var userResponse = new UserResponse();
        //    if (ModelState.IsValid)
        //    {
        //        string phoneNumber = "+1234567890";
        //        //Check if User Exist
        //        var users = await _userManager.Users.ToListAsync();
        //        var userExist = users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

        //        if (userExist != null)
        //        {
        //            return StatusCode(StatusCodes.Status403Forbidden,
        //            new UserResponse { Statut = (int)HttpStatusCode.OK, Message ="L' utilisateur exist déja!"  });
        //        }

        //        //Add the user in the database
        //        IdentityUser user = new()
        //        {
        //            PhoneNumber = registerUser.ContactDuCompte,
        //            UserName = registerUser.Pseudo,
        //            SecurityStamp = Guid.NewGuid().ToString(),
        //        };

        //        var userDtoToData = _mapper.Map<User, Data.Users>(anUser);

        //        var result = await _userManager.CreateAsync(user, registerUser.CodeSecret);
        //        return result.Succeeded
        //            ? StatusCode(StatusCodes.Status201Created,
        //                new UserResponse { Statut = (int)HttpStatusCode.OK, Message = "Enregistrement effectué avec succes"})
        //            : StatusCode(StatusCodes.Status500InternalServerError,
        //                new UserResponse { Statut = (int)HttpStatusCode.NotAcceptable, Message = "L'enregistrement à échoué"});







        //        // Accédez aux fichiers image envoyés
        //        var faceImage = anUser.PhotoIdentiteFacePath;
        //        var backImage = anUser.PhotoIdentiteArrierePath;



        //        // Enregistrez les images et obtenez leurs chemins de stockage
        //        //var faceImagePath = SaveImageAndGetPath(faceImage);
        //        //var backImagePath = SaveImageAndGetPath(backImage);

        //        // Créez l'utilisateur avec les chemins d'image
        //        //anUser.PhotoIdentiteFacePathStr = faceImagePath;
        //        //anUser.PhotoIdentiteArrierePathStr = backImagePath;



        //        userResponse.isSaved = _repo.CreateUser(userDtoToData);
        //        userResponse.Message = "Effectuer avec succès";
        //        userResponse.Statut = (int)HttpStatusCode.OK;


        //    }
        //    else
        //    {
        //        userResponse.isSaved = false;
        //    }
        //    return null;// userResponse;
        //}


        [HttpPost("AccountAlreadyExist")]
        public async Task<ActionResult> AccountAlreadyExist([FromBody]string number)
        
        {
            var userResponse = new UserResponse();

            var users = await _userManager.Users.ToListAsync();
            var userExist = users.FirstOrDefault(u => u.PhoneNumber == number);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new UserResponse { Statut = (int)HttpStatusCode.OK,userExist = true, Message = "L'utilisateur existe déjà!" });
            }

            return Ok(new UserResponse { Statut = (int)HttpStatusCode.OK,userExist = false, Message = "Nouveau utilisateur" });
        }

        //g POST api/<UserController>
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] Models.UserDto? registerUser)
        {
            var userResponse = new UserResponse();

            //// Validez si le modèle est correct
            if (!ModelState.IsValid)
            {
                return BadRequest("Modèle invalide");
            }

            // Utilisez les données de 'registerUser' pour vérifier si l'utilisateur existe

            var users = await _userManager.Users.ToListAsync();
            var userExist = users.FirstOrDefault(u => u.PhoneNumber == registerUser.PhoneNumber);


            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    new UserResponse { Statut = (int)HttpStatusCode.Forbidden, Message = "L'utilisateur existe déjà!" });
            }

            // Créez l'objet IdentityUser
            Data.User user = new()
            {
                Nom = registerUser.Nom,
                Prenoms = registerUser.Prenoms,
                NomComplet = registerUser.NomComplet,
                CodeSecret = registerUser.CodeSecret,
                DateNaissance = registerUser.DateNaissance,
                Email = registerUser.Email,
                PhotoIdentiteArrierePathStr = registerUser.PhotoIdentiteArrierePathStr,
                PhotoIdentiteFacePathStr = registerUser.PhotoIdentiteFacePathStr,
                  
                PhoneNumber = registerUser.PhoneNumber,
                UserName = registerUser.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                
            };

            // Créez l'utilisateur dans la base de données
            var result = await _userManager.CreateAsync(user, registerUser.CodeSecret);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new UserResponse { Statut = (int)HttpStatusCode.InternalServerError, Message = "L'enregistrement a échoué" });
            }

            // Mappez 'anUser' dans 'userDtoToData'
            var userDtoToData = _mapper.Map<UserDto, Data.User>(registerUser);

            // Enregistrez les informations supplémentaires avec 'userDtoToData'
            var isSaved = _repo.CreateUser(userDtoToData);
            if (!isSaved)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new UserResponse { Statut = (int)HttpStatusCode.InternalServerError, Message = "L'enregistrement des données supplémentaires a échoué" });
            }

            return Ok(new UserResponse { Statut = (int)HttpStatusCode.OK, Message = "Enregistrement effectué avec succès" });
        }




    }
}
