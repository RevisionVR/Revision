﻿using Revision.Domain.Commons;

namespace Revision.Domain.Entities.Assets;

public class Asset : Auditable
{
    public string FileName { get; set; } = string.Empty;
    public string FIlePath { get; set; } = string.Empty;
}