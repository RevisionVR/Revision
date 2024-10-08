﻿using Revision.Domain.Commons;
using Revision.Domain.Entities.Addresses;
using Revision.Domain.Entities.Devices;
using Revision.Domain.Entities.Payments;

namespace Revision.Domain.Entities.Educations;

public class Education : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public long AddressId { get; set; }
    public Address Address { get; set; }

    public long EducationCategoryId { get; set; }
    public EducationCategory EducationCategory { get; set; }

    public ICollection<Device> Devices { get; set; }
    public ICollection<DevicePayment> DevicePayments { get; set; }
    public ICollection<TopicPayment> TopicPayments { get; set; }
}