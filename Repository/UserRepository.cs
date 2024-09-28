using ProjectMaCaisseAPI_V01.Data;
using ProjectMaCaisseAPI_V01.IRepository;

namespace ProjectMaCaisseAPI_V01.Repository;

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;


        public UserRepository(DataContext context) => _context = context;

        public bool CreateUser(User? user)
        {
            if (user == null)
                return false;

            _context.Users.Add(user);
            var data = _context.SaveChanges();

            return data > 0; 
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsers()
        {
          var AllReponse = _context.Users.ToList();
          return AllReponse;    
        }

        public List<User> GetUsersById(int UserId)
        {
          var userById = _context.Users.Where(u => u.UserID == UserId).ToList();
           return userById;
        }

        public bool UpdateUser(User user)
        {
          if (user == null) return false;

          var entityUser = _context.Users.Find(user.UserID);
            entityUser.PhoneNumber = user.PhoneNumber;

            var data = _context.SaveChanges();
            return data > 0;

        }
    }

