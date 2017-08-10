using System;
using System.Configuration;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using MassTransit.NLogIntegration;
using Microsoft.ServiceBus;
using PassThePotato.Monitor.Consumers;

namespace PassThePotato.Monitor
{
    public static class ServiceBus
    {
        private static IBusControl _busControl;

        public static IBusControl Bus
        {
            get
            {
                if (_busControl == null)
                {
                    Startup().Wait();
                }

                return _busControl;
            }
        }

        public static async Task Startup()
        {
            await InitBus();
        }

        private static async Task InitBus()
        {
            _busControl = MassTransit.Bus.Factory.CreateUsingAzureServiceBus(cfg =>
            {
                var host = cfg.Host(new Uri(ConfigurationManager.AppSettings["ServiceBusUri"]), h =>
                {
                    h.TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(ConfigurationManager.AppSettings["ServiceBusLogin"],
                        ConfigurationManager.AppSettings["ServiceBusPassword"]);
                });

                cfg.UseNLog();

                cfg.ReceiveEndpoint(host, "PassThePotato.Monitor-CurrentUsersInOrderConsumer", e => e.Consumer<CurrentUsersInOrderConsumer>());
                cfg.ReceiveEndpoint(host, "PassThePotato.Monitor-WhoHasThePotatoConsumer", e => e.Consumer<WhoHasThePotatoConsumer>());
                cfg.ReceiveEndpoint(host, "PassThePotato.Monitor-StatsConsumer", e => e.Consumer<StatsConsumer>());
                
            });

            await _busControl.StartAsync();
        }

        public static async Task ShutdownBus()
        {
            if (_busControl != null)
            {
                await _busControl.StopAsync();
            }
        }
    }
}
