using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF_Xplorer.HostBuilders;
using WPF_Xplorer.View;

namespace WPF_Xplorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args).AddConfig().AddServices().AddViewModels().AddViews();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var windowMain = host.Services.GetService<MainWindow>();
            windowMain.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await host.StopAsync();
            host.Dispose();
            base.OnExit(e);
        }
    }
}
