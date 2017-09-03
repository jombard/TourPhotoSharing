namespace TPS.Web.Core.Dtos
{
    public class AuditDto
    {
        public string AuditId { get; set; }

        public string SessionId { get; set; }

        public string UserName { get; set; }

        public string IPAddress { get; set; }

        public string AreaAccessed { get; set; }

        public string Timestamp { get; set; }

        public string Data { get; set; }
    }
}