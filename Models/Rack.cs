using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Web.Models;

[Table("Racks")]
public partial class Rack
{
    public int RackId { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<Shelf> Shelves { get; set; } = new List<Shelf>();

}
