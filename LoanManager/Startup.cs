using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoanManager.Startup))]
namespace LoanManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
