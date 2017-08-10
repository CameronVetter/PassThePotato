using System;
using System.Configuration;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.AzureServiceBusTransport;
using MassTransit.NLogIntegration;
using Microsoft.ServiceBus;
using PassThePotato.Distributor.Consumers;

namespace PassThePotato.Distributor
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

                cfg.ReceiveEndpoint(host, "PassThePotato.Distributor-CurrentUsersInOrderConsumer", e => e.Consumer<CurrentUsersInOrderConsumer>());
                cfg.ReceiveEndpoint(host, "PassThePotato.Distributor-RequestWhoHasPotatoConsumer", e => e.Consumer<RequestWhoHasPotatoConsumer>());
                cfg.ReceiveEndpoint(host, "PassThePotato.Distributor-RequestWhoCanIPassToConsumer", e => e.Consumer<RequestWhoCanIPassToConsumer>());
                cfg.ReceiveEndpoint(host, "PassThePotato.Distributor-PassPotatoConsumer", e => e.Consumer<PassPotatoConsumer>());
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