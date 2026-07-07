using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Weighbridge
{
    public class PagedResultBarge
    {
        public List<BargeBaskoulViewModel> bargeBaskoulViews { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int StartEntry {get; set;}
        public int EndEntry { get; set; }
        public int Type { get; set; }
        public int CurrentPage { get; set; }
        public string SearchTerm { get; set; }
        public string Company { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int? SiteId { get; set; }
    }
}

