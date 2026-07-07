using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.Weighbridge
{
    public class WeighbridgeDomainViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScaleCode { get; set; }
        public string? CodMarkaz { get; set; }
        public int WeighbridgeSiteId { get; set; }

        /// <summary>
        ///  1: incoming,
        ///  2: outgoing
        /// </summary>
        public int? Type { get; set; }
    }
}

