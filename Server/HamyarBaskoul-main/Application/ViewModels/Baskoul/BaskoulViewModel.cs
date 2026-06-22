using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Baskoul
{
    public class BaskoulViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string ScaleCode { get; set; }
        public string? Weight { get; set; }
        // Foreign Key
        public string UserID { get; set; }
        public string? CodMarkaz { get; set; }
        public int? Site { get; set; }
        /// <summary>
        ///  1: incoming,
        ///  2: outgoing
        /// </summary>
        public int? Type { get; set; }
    }
}

