namespace Application.Test
{
    internal static class TimeConstants
    {
        public static readonly DateTime Utc9am;
        public static readonly DateTime Utc5pm;
        static TimeConstants()
        {
            DateTime utcNow = DateTime.UtcNow;


            DateTime utcMidnight = new DateTime(utcNow.Year,
                utcNow.Month,
                utcNow.Day, 0, 0, 0,
                DateTimeKind.Utc);

            Utc9am = utcMidnight.AddHours(9);//11:30 in Tehran 
            Utc5pm = utcMidnight.AddHours(17);//20:30 in Tehran
        }
    }
}
