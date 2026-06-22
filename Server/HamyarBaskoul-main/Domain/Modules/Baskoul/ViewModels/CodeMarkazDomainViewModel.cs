using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class CodeMarkazDomainViewModel
    {
        public int Id { get; set; }
        public string? CodMarkaz { get; set; }
        public string? MarkazURL { get; set; }
        public string? CoName { get; set; }
        public string? APIURL { get; set; }
        public bool? AutoAsync { get; set; }
    }
}

