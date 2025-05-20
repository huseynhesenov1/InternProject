using Project.Core.Entities.Commons;

namespace Project.Core.Entities
{
    public class Campaign : BaseAuditableEntity
    {
        public string Name { get; set; } 
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public decimal DiscountPercent { get; set; }
        public ICollection<Product> Products { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
