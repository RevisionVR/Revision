﻿using Revision.Domain.Commons;
using Revision.Domain.Enums;

namespace Revision.Service.DTOs.Users;

public class UserResultDto : Auditable
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public Role Role { get; set; }
}