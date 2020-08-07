using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HotelApp.Models;
using HotelApp.DAL;
using HotelApp.DAL.EFCore;
using HotelApp.BLL;
using HotelApp.BLL.Implementation;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HotelDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalDB")));

            services.AddTransient(typeof(MainWindow));

            services.AddScoped<ICustomerData, CustomerDataEF>();
            services.AddScoped<IReservationData, ReservationDataEF>();
            services.AddScoped<IRoomData, RoomDataEF>();
            services.AddScoped<IUnitOfWork, UnitOfWorkEF>();

            services.AddScoped<IBusinessLogic, BusinessLogic>();
        }
    }
}
