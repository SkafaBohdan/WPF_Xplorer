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
                services.AddScoped(CreatePdfTronInitializer);
                services.AddScoped<IPdfObjectValueProc, PdfObjectValueProc>();
                services.AddScoped(CreatePdfObjectProcessor);
                services.AddScoped(CreateStreamService);
                services.AddScoped(CreatePdfTreeProcessor);
                services.AddScoped(CreatePdfTronDoc);
                services.AddScoped(CreatePdfService);
                services.AddScoped(CreatePdfDocProc);
            });

            return host;
        }

        private static IPdfTronConfigurator CreatePdfTronConfigurator(IServiceProvider provider)
        {
            return new PdfTronConfigurator(hostContext.Configuration["TronKey"]);
        }

        private static IPdfTronInitializer CreatePdfTronInitializer(IServiceProvider provider)
        {
            var configurator = provider.GetRequiredService<IPdfTronConfigurator>();

            return new PdfTronInitializer(configurator);
        }

        private static IPdfObjProc CreatePdfObjectProcessor(IServiceProvider provider)
        {
            var valueProc = provider.GetRequiredService<IPdfObjectValueProc>();

            return new PdfObjectProc(valueProc);
        }

        private static IPdfService CreatePdfService(IServiceProvider provider)
        {
            var pdfTronService = provider.GetRequiredService<IPdfTronService>();
            var pdfTreeProc = provider.GetRequiredService<IPdfTreeProc>();

            return new PdfService(pdfTronService, pdfTreeProc);
        }

        public static IPdfTreeProc CreatePdfTreeProcessor(IServiceProvider provider)
        {
            var objectProc = provider.GetRequiredService<IPdfObjProc>();

            return new PdfTreeProc(objectProc);
        }

        private static IStreamService CreateStreamService(IServiceProvider provider)
        {
            var PathDoc = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var PathStream = Path.Combine(PathDoc, "tempStreams");

            return new StreamService(PathStream);
        }

        private static IPdfTronService CreatePdfTronDoc(IServiceProvider provider)
        {
            var initializer = provider.GetRequiredService<IPdfTronInitializer>();
            var pdfTreeProcessor = provider.GetRequiredService<IPdfTreeProc>();
            var streamService = provider.GetRequiredService<IStreamService>();

            return new PdfTronService(initializer, pdfTreeProcessor, streamService);
        }


        private static IPdfDocProc CreatePdfDocProc(IServiceProvider provider)
        {
            var pdfService = provider.GetRequiredService<IPdfService>();

            return new PdfDocProc(pdfService);
        }

    }
}