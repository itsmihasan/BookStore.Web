using System;
using System.Collections.Generic;

namespace BookStore.Web.Models;

public partial class Shelf
{
    public int ShelfId { get; set; }

    public string Code { get; set; } = null!;

    public int RackId { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual Rack Rack { get; set; } = null!;
}
