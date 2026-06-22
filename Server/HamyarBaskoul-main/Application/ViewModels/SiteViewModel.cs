using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SiteViewModel
    {
        public int ID { get; set; }
        public string? name { get; set; }
        public string? CodeMarkaz { get; set; }
        public bool isActive { get; set; } = true;
    }
}

