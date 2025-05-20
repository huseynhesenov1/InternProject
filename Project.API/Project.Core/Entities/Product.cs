using Project.Core.Entities.Commons;

namespace Project.Core.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Campaign? Campaign { get; set; }
        public int? CampaignId { get; set; }
    }
}
