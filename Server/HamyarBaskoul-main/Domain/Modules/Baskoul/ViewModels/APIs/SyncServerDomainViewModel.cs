using Domain.ViewModels.Weighbridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.APIs
{
    public class SyncServerDomainViewModel
    {
        public List<MabaniDomainViewModel> ListDore;
        public string? messageNode;
        public string? tableNameNode;
        public bool? stateNode;
    }
}

