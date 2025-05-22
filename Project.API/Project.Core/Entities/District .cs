using Project.Core.Entities.Commons;

namespace Project.Core.Entities
{ 
    public class District : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Worker> Workers { get; set; }
        public ICollection<ProductDistrictPrice> ProductDistrictPrices { get; set; }

    }
}
