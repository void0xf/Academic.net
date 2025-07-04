using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppActions.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        public string Comment { get; set; }
        
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        
        [ForeignKey("User")]
        public string UserId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
} 