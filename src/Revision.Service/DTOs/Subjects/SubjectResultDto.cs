﻿using Revision.Service.DTOs.SubjectCategories;
using Revision.Service.DTOs.Topics;

namespace Revision.Service.DTOs.Subjects;

public class SubjectResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public SubjectCategoryResultDto SubjectCategory { get; set; }
    public IEnumerable<TopicResultDto> Topics { get; set; }
}