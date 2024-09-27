using System.Text.RegularExpressions;

namespace GeneralStore.Common
{
    public static class CommonShared
    {       
        public static string GetMyDateTime()
        {
            return DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
        }

        public static string GetISODateTime()
        {
            return DateTime.Now.ToString("yyyyMMddTHHmmss");
        }

        public static string GetMyDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string GetMySessionidFormat()
        {
            return "yyyyMMdd";
        }
        // SessionID like yyyymmdd
        public static string GetMySessionidFormatted(string sessionid)
        {
            if (string.IsNullOrEmpty(sessionid))
                return null;

            return Regex.Replace(sessionid, "[^0-9]", "");
        }


        public static string GetMySessionid()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static string GetDeletedPrefix()
        {
            return "DELETED_" + GetISODateTime() + "_";
        }

    }
}

