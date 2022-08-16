using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HRSystem.Controllers
{
    public class ExceptionController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IExceptionService exceptionService;

        public ExceptionController(IEmployeeService employeeService, IExceptionService exceptionService)
        {
            this.employeeService = employeeService;
            this.exceptionService = exceptionService;
        }
        [Authorize(Permissions.Exception.View)]
        public IActionResult Index()
        {
            ViewBag.AllEmployees = employeeService.GetAllEmployee();
            return View();
        }
        [HttpPost]
        [Authorize(Permissions.Exception.Create)]

        public IActionResult Save(ExceptionAttendance exception)
        {

            if (ModelState.IsValid)
            {
                exceptionService.Insert(exception);
                return RedirectToAction("Index", "Attendance");

            }
            ViewBag.AllEmployees = employeeService.GetAllEmployee();
            return View("Index", exception);
        }
    }
}
