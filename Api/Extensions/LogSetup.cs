using NLog.Extensions.Logging;

namespace Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class LogSetup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ResolveLog(this IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddNLog("NLog.config");
            });

            return services;
        }
    }
}
