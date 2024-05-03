using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_DATOS.Models
{
    public class Session
    {
        public string IdSession { get; set; }
        public string IdUsers { get; set; }
        public string Token { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
