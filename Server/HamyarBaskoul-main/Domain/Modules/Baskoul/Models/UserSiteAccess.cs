namespace Domain.Models
{
    public class UserSiteAccess : AuditableEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int SiteId { get; set; }
        public WeighbridgeSite Site { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.Now;
    }
}
