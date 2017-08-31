using System;
using System.ComponentModel.DataAnnotations;

namespace TPS.Web.Core.Models
{
    public class Audit
    {
        [Key]
        public Guid AuditId { get; set; }

        public string SessionId { get; set; }

        public string UserName { get; set; }

        public string IPAddress { get; set; }

        public string AreaAccessed { get; set; }

        public DateTime Timestamp { get; set; }

        public string Data { get; set; }

    }
}