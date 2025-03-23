using Infra.Mail;
using TorinoDeploy.Data.Email;

namespace Api.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class EmailSetup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection EmailConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailMessage, EmailMessage>();
            
            services.AddTransient<IEnvioEmail>(instance =>
            {
                var emailMessage = instance.GetService<IEmailMessage>();
                var emailConfig = configuration.GetSection(EmailConfig.EMAIL_CONFIG_SECTION).Get<EmailConfig>();
                var logger = instance.GetService<ILogger<EnvioEmail>>();
                var informacaoSmtp = new SmtpModel(emailConfig.ServidorSmtp, emailConfig.PortaSmtp);

                return new EnvioEmail(emailConfig.Origem, string.Empty, string.Empty, string.Empty, informacaoSmtp, emailMessage, logger, null);
            });

            return services;
        }
    }
}
