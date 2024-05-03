using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_DATOS.Models
{
    public class Persons
    {
        public string IdPersons { get; set; }
        public string IdPersonType { get; set; }
        public string IdIdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string IdCountry { get; set; }
        public string IdDepartment { get; set; }
        public string IdCity { get; set; }
        public string IdRegional { get; set; }
        public bool HabeasData { get; set; }
        public string IdStatus { get; set; }
        public bool isActive { get; set; }
    }
}
