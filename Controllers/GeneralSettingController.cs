using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class GeneralSettingController : Controller
    {
        private readonly IGeneralSettingService GeneralService;
        private readonly IWeeklyHolidayService WeeklyHolidayService;
        public GeneralSettingController(IGeneralSettingService GeneralService, IWeeklyHolidayService WeeklyHolidayService)
        {
            this.GeneralService = GeneralService;
            this.WeeklyHolidayService = WeeklyHolidayService;

        }
        [HttpGet]
        [Authorize(Permissions.generalSetting.View)]
        public IActionResult Index()
        {
            return View(GeneralService.GetGeneralSettingViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.generalSetting.Create)]

        public IActionResult Save(GeneralSettingViewModel NewGeneralSetting)
        {
            if (ModelState.IsValid)
            {
                GeneralService.DeleteAll();
                List<DaysWithChecked> SelectedDays = NewGeneralSetting.DaysChecked.Where(n => n.Checked).ToList();
                GeneralService.Insert(new GeneralSetting { ValueOfDiscount = NewGeneralSetting.Discount, ValueOfExtra = NewGeneralSetting.Extra });
                WeeklyHolidayService.Insert(SelectedDays);
                return RedirectToAction("Index");
            }
            return View("Index", NewGeneralSetting);
        }
    }
}
