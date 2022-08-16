using HRSystem.Repositories.WeeklyHolidayRepo;

namespace HRSystem.Services.WeeklyHolidayServ
{
    public class WeeklyHolidayService : IWeeklyHolidayService
    {
        private readonly IWeeklyHolidayRepository WeeklyHolidayRepo;
        public WeeklyHolidayService(IWeeklyHolidayRepository WeeklyHolidayRepo)
        {
            this.WeeklyHolidayRepo = WeeklyHolidayRepo;
        }
        public List<WeeklyHoliday> GetAllSelectedDays()
        {
            return WeeklyHolidayRepo.GetAllSelectedDays();
        }
        public void DeleteAll()
        {
            WeeklyHolidayRepo.DeleteAll();
        }
        public void Insert(List<DaysWithChecked> selectedDays)
        {
            WeeklyHolidayRepo.Insert(selectedDays);
        }
    }
}
