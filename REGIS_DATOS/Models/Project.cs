using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REGIS_DATOS.Models
{
    public class Project
    {
        public string IdProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public string IdStatusProject { get; set; }
        public string Comments { get; set; }
        public string IdRegional { get; set; }
        public string IdCompany { get; set; }
        public bool isActive { get; set; }
    }
}