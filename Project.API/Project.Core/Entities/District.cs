using System.ComponentModel.DataAnnotations;

namespace Project.Core.Entities
{
    public class District
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        
        // Navigation property
        public virtual ICollection<Worker> Workers { get; set; }
    }
} 