namespace Domain.Interfaces
{
    public interface ITimeService
    {
        DateTime GetLocalDateTime();
        DateTime ConvertToLocalDateTime(DateTime dateTime);
    }
}
