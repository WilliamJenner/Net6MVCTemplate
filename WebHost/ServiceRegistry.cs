namespace Polaris.NetCoreMVCTemplate.WebHost
{
    public static class ServiceRegistry
    {
        public static void AddConfig(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppConfig>(provider => configuration.GetSection("AppConfig").Bind(provider));
        }

        public static void AddRestClients(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
