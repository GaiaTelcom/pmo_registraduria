using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_DATOS.Models
{
    public class Company
    {
        public string IdCompany { get; set; }
        public string IdIdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string CompanyName { get; set; }
        public string AddressStreet { get; set; }
        public int PhoneNumber { get; set; }
        public string Url { get; set; }
        public DateTime DateRegistry { get; set; }
        public string IdCountry { get; set; }
        public string IdDepartament { get; set; }
        public string IdCity { get; set; }
        public string IdRegional { get; set; }
        public bool isActive { get; set; }

    }
}
