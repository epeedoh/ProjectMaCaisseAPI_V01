using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMaCaisseAPI_V01.Data
{
    public class User: IdentityUser
    {
        public User()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
      
     //   public string? Pseudo { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public string NomComplet { get; set; }
        public DateTime? DateNaissance { get; set; }
        public string? CodeSecret { get; set; }
       // public string Domicile { get; set; }
        //public IFormFile? PhotoIdentiteFacePath { get; set; }
        //public IFormFile? PhotoIdentiteArrierePath { get; set; }
        public string? PhotoIdentiteFacePathStr { get; set; }
        public string? PhotoIdentiteArrierePathStr { get; set; }
        public bool CompteCreate { get; set; }
        public DateTime DateCreation { get; set; }


    }
}
