using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MauiApp1.Core.Interfaces;
using MauiApp1.Infrastructure.Configuration;
using MauiApp1.Infrastructure.Data;
using MauiApp1.Infrastructure.Data.Repositories;
using MauiApp1.Services;
using MauiApp1.ViewModels;
using System.Reflection;
using MauiApp1.AppLogic.Services.Interfaces;
using MauiApp1.AppLogic.Mappings;
using MauiApp1.AppLogic.Services.Implementations;

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
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Application Layer - Services  
            builder.Services.AddScoped<IUserService, UserService>();
            // builder.Services.AddScoped<IProductService, ProductService>();

            // Presentation Layer - UI Services
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();

            // Presentation Layer - ViewModels
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<DemoPageViewModel>();
            builder.Services.AddTransient<DashboardBarViewModel>();
            builder.Services.AddTransient<AddUserPageViewModel>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<UserManagePageViewModel>();
            // builder.Services.AddTransient<UserListViewModel>();

            // Presentation Layer - Pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DemoPage>();
            builder.Services.AddTransient<DashboardBar>();
            builder.Services.AddTransient<AddUserPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<UserManagePage>();
            // builder.Services.AddTransient<UserListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
