using System.Reflection;
using System.Threading;
using ApplicationInsights.OwinExtensions;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PassThePotato.Distributor.Startup))]

namespace PassThePotato.Distributor
{
    public class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            app.UseApplicationInsights();

            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");

            app.Run(c =>
            {
                if (c.Request.Path.ToString() == "/ping")
                {
                    return c.Response.WriteAsync("Pong", token);
                }

                if (c.Request.Path.ToString() == "/version")
                {
                    return c.Response.WriteAsync(Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                        token);
                }

                return c.Response.WriteAsync("PassThePotato.Distributor Functional", token);
            });

            await ServiceBus.Startup();

            if (token != CancellationToken.None)
            {
                token.Register(async () =>
                {
                    await ServiceBus.ShutdownBus();
                });
            }

        }
    }
}
