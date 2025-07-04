using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebAppActions.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<UserBook> UserBooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
} 