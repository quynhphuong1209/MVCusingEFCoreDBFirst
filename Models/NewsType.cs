using System;
using System.Collections.Generic;

namespace MVCusingEFCoreDBFirst.Models;

public partial class NewsType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? NameAscii { get; set; }

    public string? Description { get; set; }

    public int? Sort { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? IsShow { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<News> News { get; set; } = new List<News>();
}
