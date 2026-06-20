using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class Baskoul : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScaleCode { get; set; }
        public string? CodMarkaz { get; set; }
        public int Site { get; set; }
        // Foreign Key
        public string UserID { get; set; }
        /// <summary>
        ///  1: incoming,
        ///  2: outgoing
        /// </summary>
        public int? Type { get; set; }
        // Navigation Property to Identity User
        public virtual AppUser User { get; set; }
    }
}
