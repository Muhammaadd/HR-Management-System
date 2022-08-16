namespace HRSystem.Repositories.WeeklyHolidayRepo
{
    public interface IWeeklyHolidayRepository
    {
        List<WeeklyHoliday> GetAllSelectedDays();
        void DeleteAll();
        void Insert(List<DaysWithChecked> selectedDays);
    }
}
