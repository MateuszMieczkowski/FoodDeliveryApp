﻿using Microsoft.AspNetCore.Identity;

namespace Library.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ICollection<Order>? Orders { get; set; }
}
