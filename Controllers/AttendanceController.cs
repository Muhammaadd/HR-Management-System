using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace HRSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService AttendanceService;
        private readonly IDepartmentService DepartmentService;
        public AttendanceController(IAttendanceService AttendanceService, IDepartmentService DepartmentService)
        {
            this.AttendanceService = AttendanceService;
            this.DepartmentService = DepartmentService;
        }
        [HttpGet]
        [Authorize(Permissions.attendance.View)]
        public IActionResult Index(string Errors, int status)
        {
            ViewBag.Errors = Errors;
            ViewBag.status = status;
            ViewBag.EmployeeAttendances = AttendanceService.GetEmployeeAttendances();
            return View();
        }
        [HttpPost]
        [Authorize(Permissions.attendance.Create)]

        public IActionResult AddFile(IFormFile File, [FromServices] IHostingEnvironment HostingEnviroment)
        {
            if (File == null)
                return RedirectToAction("Index");
            string FileName = $"{HostingEnviroment.WebRootPath}\\files\\{File.FileName}";
            FileInfo file = new FileInfo(FileName);
            if (!AttendanceService.GetExtensions().Any(e => e == file.Extension))
            {
                ModelState.AddModelError("invalidextension", "Invalid File Extension");
                return RedirectToAction("Index", new { status = 1 });
            }
            using (FileStream FileStream = System.IO.File.Create(FileName))
            {
                File.CopyTo(FileStream);
                FileStream.Flush();
            }
            List<AttendanceExcelViewModel> ExcelData = AttendanceService.ReadDataFromExcelSheet(FileName);
            List<int> ListOfErrors = AttendanceService.AddAttendanceToDatabase(ExcelData);
            string txt;
            if (ListOfErrors.Count == 0)
            {
                txt = "Data Succefully Inserted.";
            }
            else
            {
                txt = "Attention!! \nThis Rows";
                for (int i = 0; i < ListOfErrors.Count; i++)
                {
                    txt += i + 1;
                    txt += ", ";
                }
                txt += "have invalid data please check again.";
            }
            return RedirectToAction("Index", new { Errors = txt });
        }
        [HttpPost]
        [Authorize(Permissions.attendance.View)]

        public IActionResult Search(SearchAttendanceViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                #region Test
                //foreach(var item in ModelState.Root.Children)
                //{
                //    var x = item.ValidationState;

                //    //if (viewModel.EndDate == null && x.ToString() == "Invalid")
                //    //{
                //    //    ModelState.AddModelError("EndDate", "This Filed Is Required");
                //    //}

                //    //if (viewModel.StartDate == null && x.ToString() == "Invalid")
                //    //{
                //    //    ModelState.AddModelError("StartDate", "This Filed Is Required");
                //    //}

                //}
                #endregion
                ViewBag.EmployeeAttendances = new List<EmployeeAttendanceViewModel>();
                return View("Index", viewModel);
            }

            ViewBag.EmployeeAttendances = AttendanceService.Search(viewModel);
            return View("Index");
        }
        [HttpGet]
        [Authorize(Permissions.attendance.Delete)]

        public IActionResult DeleteAttendance([FromRoute] int Id)
        {
            AttendanceService.DeleteAttendance(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Permissions.attendance.Edit)]

        public IActionResult Edit(int Id)
        {
            Attendance NewAttendacnce = AttendanceService.GetById(Id);
            AttendanceEditViewModel attendance = new AttendanceEditViewModel { Id = Id, Start = NewAttendacnce.Start, End = NewAttendacnce.End, Date = NewAttendacnce.Date, EmployeeName = NewAttendacnce.Employee.Name };
            return View(attendance);
        }
        [HttpPost]
        [Authorize(Permissions.attendance.Edit)]

        public IActionResult Edit(AttendanceEditViewModel attendance)
        {
            AttendanceService.UpdateAttendanceViewModel(attendance, attendance.Id);
            return RedirectToAction("Index");
        }
        [Authorize(Permissions.attendance.View)]
        public IActionResult ExportToExcel()
        {
            DataTable dt = new DataTable("");
            dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Name"),
                                        new DataColumn("National Id"),
                                        new DataColumn("Date"),
                                        new DataColumn("CheckIn"),
                                        new DataColumn("CheckOut"),
                                        new DataColumn("Bouns Hours"),
                                        new DataColumn("Discount Hours") });

            var Attendances = AttendanceService.GetAll();
            foreach (var attendance in Attendances)
            {
                dt.Rows.Add(attendance.Employee.Name, attendance.Employee.NationalId, attendance.Date,
                    attendance.Start, attendance.End, attendance.BonusHours, attendance.DiscountHours);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                var WS = wb.Worksheets.Add("sheet1");
                // The false parameter indicates that a table should not be created
                WS.FirstCell().InsertTable(dt, false);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Attendance-{DateTime.Now.ToString("d")}.xlsx");
                }
            }
        }
        public IActionResult CheckStart(TimeSpan Start, TimeSpan End)
        {
            if (Start > End)
            {
                return Json(false);
            }
            return Json(true);
        }
        public IActionResult CheckEnd(TimeSpan End, TimeSpan Start)
        {
            if (End > Start)
            {
                return Json(true);
            }
            return Json(false);
        }
        public IActionResult CheckSearchDate(DateTime StartDate, DateTime EndDate)
        {
            if (EndDate > StartDate)
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
