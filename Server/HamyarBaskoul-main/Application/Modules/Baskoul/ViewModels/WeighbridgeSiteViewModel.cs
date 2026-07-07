using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class WeighbridgeSiteViewModel
    {
        public int ID { get; set; }
        public string? name { get; set; }
        public string? Company { get; set; }
        public string? CompanyName { get; set; }
        public bool isActive { get; set; } = true;
    }
}

