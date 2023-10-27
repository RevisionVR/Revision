using Revision.Domain.Constants;

namespace Revision.Service.Commons.Helpers;

public class TimeHelper
{
    public static DateTime GetDateTime()
        => DateTime.Now.AddHours(TimeConstants.UTC);
}