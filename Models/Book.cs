﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookomari.com.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Book Name")]
        public string BookName { get; set; }
        [Display(Name = "Book Language")]
        public string Language { get; set; }
        [Display(Name = "Book Cover Photo")]
        public byte[] BookCoverPhoto { get; set; }

        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
