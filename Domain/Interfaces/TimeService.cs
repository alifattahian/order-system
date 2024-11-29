namespace Domain.Interfaces
{
    public class TimeService : ITimeService
    {
        public DateTime GetLocalDateTime()
        {
            return ConvertToLocalDateTime(DateTime.Now);
        }

        public DateTime ConvertToLocalDateTime(DateTime dateTime)
        {
            TimeZoneInfo tehranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            DateTime tehranDateTime = TimeZoneInfo.ConvertTime(dateTime, tehranTimeZone);
            return tehranDateTime;
        }
    }
}
