using Application.ViewModels.Baskoul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PagedResultCodeMarkazList
    {
        public List<CodeMarkazViewModel> Markazes { get; set; }
        public int TotalCount { get; set; }
    }
}

