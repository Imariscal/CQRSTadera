﻿
using Catalog.Domain.Entities.Base;

namespace Catalog.Retail.Domain.Entities;

public class Employee : Auditable<Guid>
{        
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
