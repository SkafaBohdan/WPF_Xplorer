using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using WPF_Xplorer.Interfaces;
using WPF_Xplorer.Services;
using WPF_Xplorer.Services.Interfaces;

namespace WPF_Xplorer.HostBuilders
{
    public static class AddServicesHostBuilderExtension
    {
        private static HostBuilderContext hostContext;

        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                hostContext = context;

                services.AddScoped(CreatePdfTronConfigurator);
                services.AddScoped<IPdfTronInitializer, PdfTronInitializer>();
                services.AddScoped<IPdfObjectValueProc, PdfObjectValueProc>();
                services.AddScoped<IPdfObjProc, PdfObjectProc>();
                services.AddScoped(CreateStreamService);
                services.AddScoped<IPdfTreeProc, PdfTreeProc>();
                services.AddScoped<IPdfTronService, PdfTronService>();
                services.AddScoped<IPdfService, PdfService>();
                services.AddScoped<IPdfDocProc, PdfDocProc>();

            });

            return host;
        }

        private static IPdfTronConfigurator CreatePdfTronConfigurator(IServiceProvider provider)
        {
            return new PdfTronConfigurator(hostContext.Configuration["TronKey"]);
        }

        private static IStreamService CreateStreamService(IServiceProvider provider)
        {
            var PathDoc = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var PathStream = Path.Combine(PathDoc, "tempStreams");

            return new StreamService(PathStream);
        }

    }
}