using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revision.Service.DTOs.Notifications;

public class SmsSender
{
    public string Recipient { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
}
