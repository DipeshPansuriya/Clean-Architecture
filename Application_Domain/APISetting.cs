namespace Application_Domain
{
    public class APISetting
    {
        public static string DBConnection { get; set; }
        public static string CORSAllowOrigin { get; set; }
        public static EmailConfiguration EmailConfiguration { get; set; }
        public static CacheSetting CacheConfiguration { get; set; }
        public static string XMLFilePath { get; set; }
    }

    public class EmailConfiguration
    {
        public string SMTPAddress { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string SMTPSecure { get; set; }
    }

    public class CacheSetting
    {
        public double AbsoluteExpirationInHours { get; set; }
        public double SlidingExpirationInMinutes { get; set; }
    }
}