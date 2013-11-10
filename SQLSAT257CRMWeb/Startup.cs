using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SQLSAT257CRMWeb.Startup))]
namespace SQLSAT257CRMWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
