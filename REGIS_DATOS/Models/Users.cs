using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace REGIS_DATOS.Models
{
    public class Users
    {
        public string IdUsers { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IdRole { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastAccessed { get; set; }
        public bool Confirmed { get; set; }
        public bool Reinstate { get; set; }
        public string IdRegional { get; set; }
        public string IdPerson { get; set; }
        public bool isActive { get; set; }

        //Token sesion
        public string Token { get; set; }
    }
}

