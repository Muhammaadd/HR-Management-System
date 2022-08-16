using HRSystem.Models;

namespace HRSystem.Repositories.WeeklyHolidayRepo
{
    public class WeeklyHolidayRepository : IWeeklyHolidayRepository
    {
        private readonly HRDbContext context;
        public WeeklyHolidayRepository(HRDbContext context)
        {
            this.context = context;
        }
        public List<WeeklyHoliday> GetAllSelectedDays()
        {
            return context.WeeklyHolidays.ToList();
        }
        public void DeleteAll()
        {
            context.WeeklyHolidays.RemoveRange(context.WeeklyHolidays);
            context.SaveChanges();
        }
        public void Insert(List<DaysWithChecked> selectedDays)
        {
            foreach (var item in selectedDays)
            {
                context.WeeklyHolidays.Add(new WeeklyHoliday { GeneralId = 1, Day = item.Day });
            }
            context.SaveChanges();
        }
    }
}
