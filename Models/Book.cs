using System;
using System.Collections.Generic;

namespace BookStore.Web.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public int? YearOfPublish { get; set; }

    public bool IsAvailable { get; set; }

    public decimal Price { get; set; }

    public int ShelfId { get; set; }

    public virtual Shelf Shelf { get; set; } = null!;
}
