using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_DATOS.Models
{
    public class ServiceProvided
    {
        public string IdServiceProvided  { get; set; }
        public string IdCompay    { get; set; }
        public string IdService { get; set; }
        public string IdPerson { get; set; }
        public DateTime ContractDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
