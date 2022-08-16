using HRSystem.Repositories.WeeklyHolidayRepo;

namespace HRSystem.Services.WeeklyHolidayServ
{
    public interface IWeeklyHolidayService : IWeeklyHolidayRepository
    {
        List<WeeklyHoliday> GetAllSelectedDays();
        void DeleteAll();
        void Insert(List<DaysWithChecked> selectedDays);
    }
}
