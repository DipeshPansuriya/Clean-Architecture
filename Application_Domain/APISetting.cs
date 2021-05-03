namespace Application_Domain
{
    public class APISetting
    {
        public static string DBConnection { get; set; }
        public static string CORSAllowOrigin { get; set; }
        public static CacheSetting cacheConfiguration { get; set; }
        public static string XMLFilePath { get; set; }
    }

    public class CacheSetting
    {
        public double AbsoluteExpirationInHours { get; set; }
        public double SlidingExpirationInMinutes { get; set; }
    }
}