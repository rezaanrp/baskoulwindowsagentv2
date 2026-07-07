using Application.ViewModels.Weighbridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PagedResultCompanyList
    {
        public List<CompanyViewModel> Markazes { get; set; }
        public int TotalCount { get; set; }
    }
}

