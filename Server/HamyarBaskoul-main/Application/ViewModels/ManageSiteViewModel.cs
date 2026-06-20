using Application.ViewModels.Baskoul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ManageSiteViewModel
    {
        public IEnumerable<BaskoulViewModel> baskoulViews { set; get; }
        public List<SiteViewModel> siteViews { set; get; }
        public string SiteName { get; set; }
    }
}
