using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppActions.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        [ForeignKey("Category")]
        public Guid? CategoryId { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public virtual Category Category { get; set; }
        public virtual ICollection<UserBook> UserBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
} 