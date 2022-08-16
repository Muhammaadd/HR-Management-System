using HRSystem.Repositories.GeneralSettingRepo;

namespace HRSystem.Services.GeneralSettingServ
{
    public class GeneralSettingService : IGeneralSettingService
    {
        private readonly IGeneralSettingRepository GeneralRepo;
        private readonly IWeeklyHolidayRepository WeeklyHolidayRepo;
        public GeneralSettingService(IGeneralSettingRepository GeneralRepo, IWeeklyHolidayRepository WeeklyHolidayRepo)
        {
            this.GeneralRepo = GeneralRepo;
            this.WeeklyHolidayRepo = WeeklyHolidayRepo;
        }

        public List<GeneralSetting> GetAll()
        {
            return GeneralRepo.GetAll();
        }
        public void DeleteAll()
        {
            WeeklyHolidayRepo.DeleteAll();
        }
        public GeneralSetting GetById(int id)
        {
            return GeneralRepo.GetById(id);
        }
        public void Insert(GeneralSetting NewGeneralSetting)
        {
            GeneralRepo.Insert(NewGeneralSetting);
        }
        public GeneralSettingViewModel GetGeneralSettingViewModel()
        {
            GeneralSettingViewModel? generalSettingViewModel = new GeneralSettingViewModel();
            generalSettingViewModel.Extra = GetAll().Select(n => n.ValueOfExtra).FirstOrDefault();
            generalSettingViewModel.Discount = GetAll().Select(n => n.ValueOfDiscount).FirstOrDefault();
            List<string> SelectedDays = WeeklyHolidayRepo.GetAllSelectedDays().Select(n => n.Day).ToList();
            List<string> AllDays = GetWeekDays();
            var DaysChecked = AllDays.Select(n => new DaysWithChecked { Day = n }).ToList();
            foreach (var item in DaysChecked)
            {
                if (SelectedDays.Any(n => n == item.Day))
                {
                    item.Checked = true;
                }
                else
                {
                    item.Checked = false;
                }
            }
            generalSettingViewModel.DaysChecked = DaysChecked;
            return generalSettingViewModel;
        }
        // Get Days of the Week
        public List<string> GetWeekDays()
        {
            return new List<string>
            {
                "Saturday",
                "Sunday",
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday"
            };
        }
        public int OverTimePricePerHour()
        {
            return GeneralRepo.OverTimePricePerHour();
        }
        public int DeductionPricePerHour()
        {
            return GeneralRepo.DeductionPricePerHour();
        }
    }
}
