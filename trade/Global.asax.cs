using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace trade
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start() { GlobalConfiguration.Configure(WebApiConfig.Register); }
        protected void Application_PostAuthorizeRequest() { HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required); }
    }
}
