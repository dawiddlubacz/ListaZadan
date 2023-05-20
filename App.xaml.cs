using ListaZadan.BazaDanych;
using ListaZadan.Widoki;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ListaZadan
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider ServiceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(@"Data Source=(localdb)\ListaZadan;Initial Catalog=ListaZadan;");
            });
            services.AddSingleton<MainWindow>();
            services.AddSingleton<Zaloguj>();
            services.AddSingleton<ZalozKonto>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetService<MainWindow>();
            var zalozKonto = new ZalozKonto(mainWindow);
            var zaloguj = new Zaloguj(mainWindow);
            zaloguj = ServiceProvider.GetService<Zaloguj>();
            zalozKonto = ServiceProvider.GetService<ZalozKonto>();
            zaloguj.Show();
        }

    }
}
