using System.ComponentModel.DataAnnotations;
using Project.Core.Entities.Commons;

namespace Project.Core.Entities
{
    public class Worker : BaseAuditableEntity
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(7)]
        public string FinCode { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        [Required]
        public int DistrictId { get; set; }
        
        public string WorkerToken { get; set; }
        
        // Navigation property
        public virtual District District { get; set; }
    }
}
