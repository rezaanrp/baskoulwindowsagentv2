using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.BaseData
{
    public class ListPageViewModel
    {
        public int type { get; set; }
        public string? name { get; set; }
        public string? des { get; set; }
        public List<BaseDataViewModel>? data { get; set; }
    }
}
