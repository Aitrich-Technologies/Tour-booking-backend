using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Email.Helper
{
    public class MailRequest
    {
        public string ToEmail { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public List<AttachmentFile>? Attachments { get; set; }
    }

    public class AttachmentFile
    {
        public string FileName { get; set; } = null!;
        public byte[] FileBytes { get; set; } = null!;
        public string ContentType { get; set; } = "application/pdf";
    }
}
