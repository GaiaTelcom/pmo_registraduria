using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_DATOS.Models
{
    public class Log
    {
        public string IdLog { get; set; }
        public string IdUsers { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string Accion { get; set; }
    }
}
