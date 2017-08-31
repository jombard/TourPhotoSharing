using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using TPS.Web.Persistence;

namespace TPS.Web.Core.Models
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            var userCookie = request.Cookies[FormsAuthentication.FormsCookieName]?.Value ?? "";
            var sessionIdentifier = string.Join("", MD5.Create()
                    .ComputeHash(Encoding.ASCII.GetBytes(userCookie))
                    .Select(s => s.ToString("x2")));

            var audit = new Audit
            {
                AuditId = Guid.NewGuid(),
                UserName = request.IsAuthenticated ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                AreaAccessed = request.RawUrl,
                Timestamp = DateTime.UtcNow,
                SessionId = sessionIdentifier,
                Data = SerializeRequest(request)
            };

            var context = new ApplicationDbContext();
            context.AuditRecords.Add(audit);
            context.SaveChanges();

            base.OnActionExecuting(filterContext);
        }

        private string SerializeRequest(HttpRequestBase request)
        {
            return Json.Encode(new
            {
                request.Cookies,
                request.Headers,
                request.Files,
                request.QueryString,
                request.Params
            });
        }
    }
}