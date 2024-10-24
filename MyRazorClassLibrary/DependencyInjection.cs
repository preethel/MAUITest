using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace MyRazorClassLibrary
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMyRazorClassLibraryServices(this IServiceCollection services, IConfiguration configuration, bool IsAndroidLocal = false)
        {
            var baseAddress = IsAndroidLocal ? configuration["ApiSettings:LocalAndroidBaseAddress"] : configuration["ApiSettings:BaseAddress"];
            var apiBaseAddress = new Uri(baseAddress ?? throw new InvalidOperationException("Api:BaseAddress"));
            services.AddRefitClient<ITodoService>()
                .ConfigureHttpClient((serviceProvider, httpClient) =>
                {
                    httpClient.BaseAddress = apiBaseAddress;
                });

            //services.AddHttpClient<ITodoService, TodoService>(client =>
            //{
            //    client.BaseAddress = new Uri(configuration["ApiSettings:BaseAddress"]);
            //});

            return services;
        }
    }
}
