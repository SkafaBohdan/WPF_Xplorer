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
                services.AddSingleton(CreateBookmarkViewModel);
            });

            return host;
        }

        private static ApplicationMainWindowViewModel CreatePdfViewModel(IServiceProvider provider)
        {
            var docProc = provider.GetRequiredService<IPdfDocProc>();
            var viewModel = provider.GetRequiredService<BookmarkUpdateViewModel>();

            return new ApplicationMainWindowViewModel(docProc, viewModel);
        }
        private static BookmarkUpdateViewModel CreateBookmarkViewModel(IServiceProvider provider)
        {
            var bookService = provider.GetRequiredService<IBookmarksUpdateService>();
            return new BookmarkUpdateViewModel(bookService);
        }
    }
}
