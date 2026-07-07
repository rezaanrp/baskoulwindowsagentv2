using Application.ViewModels.Weighbridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ManageWeighbridgeSiteViewModel
    {
        public IEnumerable<WeighbridgeViewModel> baskoulViews { set; get; }
        public List<WeighbridgeSiteViewModel> siteViews { set; get; }
        public string SiteName { get; set; }
    }
}

