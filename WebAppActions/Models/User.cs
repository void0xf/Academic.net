using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppActions.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<UserBook> UserBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
} 