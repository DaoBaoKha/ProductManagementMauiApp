using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MauiApp1.Core.Interfaces;
using MauiApp1.Infrastructure.Configuration;
using MauiApp1.Infrastructure.Data;
using MauiApp1.Infrastructure.Data.Repositories;
using MauiApp1.Services;
using MauiApp1.ViewModels;
using System.Reflection;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
{
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configuration
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("MauiApp1.appsettings.json");
            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                
                builder.Configuration.AddConfiguration(config);
            }

            // MongoDB Configuration
            builder.Services.Configure<MongoDbSettings>(options =>
                builder.Configuration.GetSection("MongoDb").Bind(options));

            // Infrastructure Layer - Data Access
            builder.Services.AddSingleton<MongoDbContext>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

            // Application Layer - AutoMapper
            builder.Services.AddAutoMapper(typeof(MauiApp1.AppLogic.Mappings.MappingProfile));

            // Application Layer - Services  
            builder.Services.AddScoped<MauiApp1.AppLogic.Services.Interfaces.IUserService, 
                MauiApp1.AppLogic.Services.Implementations.UserService>();
            // TODO: Add more services here as you create them
            // builder.Services.AddScoped<IProductService, ProductService>();

            // Presentation Layer - UI Services
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();

            // Presentation Layer - ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<DemoPageViewModel>();
            builder.Services.AddTransient<DashboardBarViewModel>();
            // TODO: Add more ViewModels here
            // builder.Services.AddTransient<UserListViewModel>();

            // Presentation Layer - Pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DemoPage>();
            builder.Services.AddTransient<DashboardBar>();
            // TODO: Add more pages here
            // builder.Services.AddTransient<UserListPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
