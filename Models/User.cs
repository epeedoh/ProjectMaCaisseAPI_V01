using Microsoft.AspNetCore.Identity;

namespace ProjectMaCaisseAPI_V01.Models
{
    public class UserDto
    {
        public int? UserID { get; set; }
        public string? Nom { get; set; }
        public string? Prenoms { get; set; }
        public string? NomComplet => $"{Prenoms} {Nom}";
        public DateTime? DateNaissance { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? CodeSecret { get; set; }
        public string? PhotoIdentiteFacePathStr { get; set; }
        public string? PhotoIdentiteArrierePathStr { get; set; }
        public virtual string? UserName { get; set; }
        public bool? CompteCreate { get; set; }
        public DateTime? DateCreation { get; set; }

    }
}
