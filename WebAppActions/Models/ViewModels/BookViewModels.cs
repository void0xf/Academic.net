using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebAppActions.Models.ViewModels
{
    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class BookFormViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Author must be between 1 and 100 characters.")]
        public string Author { get; set; }

        public Guid? CategoryId { get; set; }
    
        public SelectList? CategoryList { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public class ReviewViewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class BookOwnerViewModel
    {
        public string Username { get; set; }
    }

    public class BookDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
        public IEnumerable<BookOwnerViewModel> Owners { get; set; }
    }
} 