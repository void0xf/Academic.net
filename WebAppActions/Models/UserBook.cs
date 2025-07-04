using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppActions.Models
{
    public class UserBook
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
} 