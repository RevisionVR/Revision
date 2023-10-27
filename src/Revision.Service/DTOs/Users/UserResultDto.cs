﻿using Revision.Domain.Enums;
using Revision.Service.DTOs.Addresses;

namespace Revision.Service.DTOs.Users;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public Role Role { get; set; }
    public AddressResultDto Address { get; set; }
}