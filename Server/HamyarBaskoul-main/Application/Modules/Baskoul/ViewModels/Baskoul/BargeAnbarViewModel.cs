using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Weighbridge
{
    public class BargeAnbarViewModel
    {
        public IEnumerable<BargeBaskoulViewModel>? BargeBaskouls;
        public IEnumerable<WeighbridgeViewModel>? Baskouls;
        public BargeBaskoulViewModel? BargeAnbar;
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public string? Codemarkaz { get; set; }
    }
}

