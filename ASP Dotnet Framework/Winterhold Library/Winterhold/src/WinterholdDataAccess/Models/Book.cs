﻿using System;
using System.Collections.Generic;

namespace WinterholdDataAccess.Models
{
    public partial class Book
    {
        public Book()
        {
            Loans = new HashSet<Loan>();
        }

        public string Code { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public long AuthorId { get; set; }
        public bool IsBorrowed { get; set; }
        public string? Summary { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? TotalPage { get; set; }

        public virtual Author Author { get; set; } = null!;
        public virtual Category CategoryNameNavigation { get; set; } = null!;
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
