using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WPF_Xplorer.Services.Interfaces;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.HostBuilders
{
    public static class AddViewModelsHostBuilderExtension
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton(CreatePdfViewModel);
            });

            return host;
        }

        private static ApplicationMainWindowViewModel CreatePdfViewModel(IServiceProvider provider)
        {
            var docProc = provider.GetRequiredService<IPdfDocProc>();
            return new ApplicationMainWindowViewModel(docProc);
        }
    }
}
