using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyRazorClassLibrary;
using System.Reflection;

namespace MyMauiApp
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
                });

            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".appsettings.json");

            var configuration = new ConfigurationBuilder()
                .AddJsonStream(stream!)
                .Build();

            builder.Configuration.AddConfiguration(configuration);

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMyRazorClassLibraryServices(configuration);
#if DEBUG
            builder.Services.AddMyRazorClassLibraryServices(configuration, true);
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<MainPage>();
            var app = builder.Build();

            return app;
        }
    }
}
