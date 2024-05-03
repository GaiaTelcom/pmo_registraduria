using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_DATOS.Models
{
    public  class Role
    {
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IdPermission { get; set; }
        public string IdState { get; set; }
    }
}
