using Project.Core.Entities.Commons;

namespace Project.Core.Entities
{
    public class Worker : BaseAuditableEntity
    {
        public string FinCode { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int DistrictId { get; set; }
    }
}
