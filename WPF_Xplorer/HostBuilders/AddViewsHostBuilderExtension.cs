using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF_Xplorer.View;
using WPF_Xplorer.ViewModels;

namespace WPF_Xplorer.HostBuilders
{
    public static class AddViewsHostBuilderExtension
    {
        public static IHostBuilder AddViews(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton(provider => new MainWindow(provider.GetRequiredService<ApplicationMainWindowViewModel>()));
                services.AddScoped(provider => new BookmarkListWindow(provider.GetRequiredService<BookmarksViewModel>()));
                services.AddSingleton(provider => new BookmarkUpdateWindow(provider.GetRequiredService<BookmarkUpdateViewModel>()));
            });

            return host;
        }
    }
}
