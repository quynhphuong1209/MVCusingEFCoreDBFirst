using System;
using System.Collections.Generic;

namespace MVCusingEFCoreDBFirst.Models;

public partial class News
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public string? Author { get; set; }

    public string? ImageUrl { get; set; }

    public int? TypeId { get; set; }

    public string? Summary { get; set; }

    public bool? IsDeleted { get; set; }

    public bool? IsShow { get; set; }

    public virtual NewsType? Type { get; set; }
}
