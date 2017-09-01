using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace TourPhotoSharing.Tests
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUser(this ApiController controller, string userId, string username)
        {
            var identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userId));

            var principal = new GenericPrincipal(identity, null);

            controller.User = principal;
        }

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "https://localhost:44349/", "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState)
                .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Standard,
                    new[] {typeof(HttpSessionStateContainer)}, null).Invoke(new object[] {sessionContainer});

            return httpContext;
        }
    }
}
