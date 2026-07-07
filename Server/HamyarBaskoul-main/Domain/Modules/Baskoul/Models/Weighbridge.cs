namespace Domain.Models
{
    public class Weighbridge : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScaleCode { get; set; }
        public string? CodMarkaz { get; set; }
        public int WeighbridgeSiteId { get; set; }
        public WeighbridgeSite WeighbridgeSite { get; set; }

        /// <summary>
        ///  1: incoming,
        ///  2: outgoing
        /// </summary>
        public int? Type { get; set; }
    }
}
