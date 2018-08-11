using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Form_Builder_App.Startup))]
namespace Form_Builder_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
