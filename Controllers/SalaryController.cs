using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalaryService salaryService;
        private readonly IEmployeeService employeeService;
        private readonly IAttendanceService attendanceService;

        public SalaryController(ISalaryService salaryService, IEmployeeService employeeService, IAttendanceService attendanceService)
        {
            this.salaryService = salaryService;
            this.employeeService = employeeService;
            this.attendanceService = attendanceService;
        }



        [HttpGet]
        [Authorize(Permissions.Salary.View)]
        public IActionResult SalaryReport(int? empId, DateTime? datemonth)

        {
            ViewBag.empId = empId;
            ViewBag.datemonth = datemonth;
            ViewBag.emplist = employeeService.GetAllEmployee();
            List<SalaryWithAttend_Emp_GenSettingsVM> ListOfEmployees = salaryService.SalaryReport();

            //   List<dateformual> DateRange = attendanceService.GetDateformuals();

            if (empId.HasValue && datemonth != null)
            {
                dateformual date = new dateformual();
                List<SalaryWithAttend_Emp_GenSettingsVM> filtredList = salaryService.SalaryReport(empId.Value, datemonth.Value);

                return View(filtredList);
            }

            else if (empId == null && datemonth != null)
            {

                if (datemonth.Value.Year < 2010)
                {
                    ModelState.AddModelError("", "plz check year ,should be after 2010 ");
                    return View(
                        new List<SalaryWithAttend_Emp_GenSettingsVM>
                        {
                            new SalaryWithAttend_Emp_GenSettingsVM()
                            {
                                filterdate=new dateformual{ Year=datemonth.Value.Year,Month=datemonth.Value.Month}
                            }
                         });
                }
                else
                {
                    dateformual? filter = new dateformual { Year = datemonth.Value.Year, Month = datemonth.Value.Month };
                    List<SalaryWithAttend_Emp_GenSettingsVM> filtrdIDlist = ListOfEmployees.Where(c => c.filterdate.Month == filter.Month && c.filterdate.Year == filter.Year).ToList();

                    return View(filtrdIDlist);
                }

            }



            else if (empId != null && datemonth == null)
            {
                List<SalaryWithAttend_Emp_GenSettingsVM> filtrdIDlist = ListOfEmployees.Where(c => c.EmployeeIdVM == empId.Value).ToList();

                return View(filtrdIDlist);
            }

            else
            {
                return View(ListOfEmployees);
            }







        }


        [HttpGet]
        [Authorize(Permissions.Salary.View)]
        public IActionResult EmpolyeeSalaryReport(int id, int targetM, int targetY)
        {

            dateformual targetDate = new dateformual { Month = targetM, Year = targetY };
            SalaryWithAttend_Emp_GenSettingsVM EmployeeSalary = salaryService.EmpolyeeSalaryReport(id, targetDate);

            return View(EmployeeSalary);

        }
    }
}
