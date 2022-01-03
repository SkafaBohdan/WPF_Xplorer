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
                services.AddScoped(CreatePdfDocProcessor);
            });

            return host;
        }

        private static IPdfTronConfigurator CreatePdfTronConfigurator(IServiceProvider provider)
        {
            return new PdfTronConfigurator(hostContext.Configuration["PdfTronKey"]);
        }

        private static IPdfTronInitializer CreatePdfTronInitializer(IServiceProvider provider)
        {
            var configurator = provider.GetRequiredService<IPdfTronConfigurator>();

            return new PdfTronInitializer(configurator);
        }

        private static IPdfObjProc CreatePdfObjectProcessor(IServiceProvider provider)
        {
            var valueProcessor = provider.GetRequiredService<IPdfObjectValueProc>();

            return new PdfObjectProc(valueProcessor);
        }

        private static IPdfService CreatePdfService(IServiceProvider provider)
        {
            var pdfTronService = provider.GetRequiredService<IPdfTronService>();
            var pdfTreeProcessor = provider.GetRequiredService<IPdfTreeProc>();

            return new Services.PdfService(pdfTronService, pdfTreeProcessor);
        }

        public static IPdfTreeProc CreatePdfTreeProcessor(IServiceProvider provider)
        {
            var objectProcessor = provider.GetRequiredService<IPdfObjProc>();

            return new PdfTreeProc(objectProcessor);
        }

        private static IStreamService CreateStreamService(IServiceProvider provider)
        {
            var docFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var streamFolderPath = Path.Combine(docFolderPath, "streams");

            return new StreamService(streamFolderPath);
        }

        private static IPdfTronService CreatePdfTronDoc(IServiceProvider provider)
        {
            var initializer = provider.GetRequiredService<IPdfTronInitializer>();
            var pdfTreeProcessor = provider.GetRequiredService<IPdfTreeProc>();
            var streamService = provider.GetRequiredService<IStreamService>();

            return new PdfTronService(initializer, pdfTreeProcessor, streamService);
        }


        private static IPdfDocProc CreatePdfDocProcessor(IServiceProvider provider)
        {
            var pdfService = provider.GetRequiredService<IPdfService>();

            return new PdfDocProc(pdfService);
        }

    }
}