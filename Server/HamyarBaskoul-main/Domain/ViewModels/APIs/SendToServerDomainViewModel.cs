using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.APIs
{
    public class SendToServerDomainViewModel
    {
        public string? Message { get; set; }

        public bool? State { get; set; }

        public int? Data { get; set; } = null;
    }
}

