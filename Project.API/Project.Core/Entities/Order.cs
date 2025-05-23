using Project.Core.Entities.Commons;

namespace Project.Core.Entities
{
    public class Order : BaseAuditableEntity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int ProductCount { get; set; }
        public int WorkerId { get; set; }
        public Worker Worker { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
