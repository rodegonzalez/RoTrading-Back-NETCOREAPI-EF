namespace BK_NetAPI_SQLite.Common
{
    public static class Common
    {       
        public static string GetMyDateTime()
        {
            return DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
        }

        public static string GetMyDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string GetMySessionid()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
    }
}

