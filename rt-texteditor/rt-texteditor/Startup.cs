using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(rt_texteditor.Startup))]
namespace rt_texteditor
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
