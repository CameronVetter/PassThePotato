using System.Reflection;
using System.Threading;
using ApplicationInsights.OwinExtensions;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using PassThePotato.Sessions.EndPoint;

[assembly: OwinStartup(typeof(Startup))]

namespace PassThePotato.Sessions.EndPoint
{
    public class Startup
    {
        public async void Configuration(IAppBuilder app)
        {
            app.UseApplicationInsights();

            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");

            // TODO this opens up cross site scripting vulnerabilities
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                map.RunSignalR(new Microsoft.AspNet.SignalR.HubConfiguration());
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
