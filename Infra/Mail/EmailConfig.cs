namespace TorinoDeploy.Data.Email
{
    public class EmailConfig
    {
        public static readonly string EMAIL_CONFIG_SECTION = "EmailConfig";

        public string Origem { get; set; }
        public string ServidorSmtp { get; set; }
        public int PortaSmtp { get; set; }
    }
}
