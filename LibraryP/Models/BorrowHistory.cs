using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryP.Models
{
    public class BorrowHistory
    {
        public int BorrowHistoryId { get; set; }
        [Required]
        [Display(Name = "Books")]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        [Display(Name = "Customers")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

    }
}