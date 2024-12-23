using System;
using System.Collections.Generic;

namespace MVCusingEFCoreDBFirst.Models;

public partial class User
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? FullName { get; set; }

    public DateTime? BirthDay { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool? IsShow { get; set; }

    public bool? IsDeleted { get; set; }
}
