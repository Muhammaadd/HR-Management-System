using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        [Authorize(Permissions.Employee.View)]
        public IActionResult Index()
        {

            List<Employee> employee = employeeService.GetAllEmployee();
            return View(employee);
        }
        [HttpGet]
        public IActionResult GetByName(string id)
        {
            List<Employee> employee = employeeService.GetEmployeeByName(id);
            return Json(employee);
        }
        [HttpGet]
        [Authorize(Permissions.Employee.View)]
        public IActionResult Details(int id)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            if (employee != null)
                return View(employee);

            return NotFound();
        }
        [HttpGet]
        [Authorize(Permissions.Employee.Create)]
        public IActionResult Add()
        {
            ViewBag.DeptList = employeeService.GetAllDepartment();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Employee.Create)]
        public IActionResult Add(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                if (employeeViewModel.Gender == null)
                {
                    if (employeeViewModel.DeptId == null)
                    {
                        ModelState.AddModelError("Gender", "Gender is required");
                        ModelState.AddModelError("DeptId", "Department is required");
                        return View(employeeViewModel);
                    }
                    else
                    {
                        ModelState.AddModelError("Gender", "Gender is required");
                        return View(employeeViewModel);
                    }

                }
                else if (employeeViewModel.DeptId == null)
                {
                    ModelState.AddModelError("DeptId", "Department is required");
                    return View(employeeViewModel);
                }
                try
                {
                    employeeService.InsertViewModel(employeeViewModel);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("Other", "Faild To Add Employee Please Try Again");
                    return View(employeeViewModel);
                }

            }
            ViewBag.DeptList = employeeService.GetAllDepartment();
            return View(employeeViewModel);

        }

        [HttpGet]
        [Authorize(Permissions.Employee.Edit)]
        public IActionResult Edit(int id)
        {
            EmployeeViewModel employeeViewModel = employeeService.GetViewModel(id);
            employeeViewModel.Id = id;
            ViewBag.DeptList = employeeService.GetAllDepartment();
            if (employeeViewModel.Gender == null)
            {
                if (employeeViewModel.DeptId == null)
                {
                    ModelState.AddModelError("Gender", "Gender is required");
                    ModelState.AddModelError("DeptId", "Department is required");
                    return View(employeeViewModel);
                }
                else
                {
                    ModelState.AddModelError("Gender", "Gender is required");
                    return View(employeeViewModel);
                }

            }
            else if (employeeViewModel.DeptId == null)
            {
                ModelState.AddModelError("DeptId", "Department is required");
                return View(employeeViewModel);
            }



            return View(employeeViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Permissions.Employee.Edit)]
        public IActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                if (employeeViewModel.Gender == "0")
                {
                    if (employeeViewModel.DeptId == null)
                    {
                        ModelState.AddModelError("Gender", "Gender is required");
                        ModelState.AddModelError("DeptId", "Department is required");
                        return View(employeeViewModel);
                    }
                    else
                    {
                        ModelState.AddModelError("Gender", "Gender is required");
                        return View(employeeViewModel);
                    }

                }
                else if (employeeViewModel.DeptId == null)
                {
                    ModelState.AddModelError("DeptId", "Department is required");
                    return View(employeeViewModel);
                }
                employeeService.UpdateEmployeeWithViewModel(employeeViewModel);
                return RedirectToAction("Index");
            }
            ViewBag.DeptList = employeeService.GetAllDepartment();

            return View(employeeViewModel);
        }
        [HttpGet]
        [Authorize(Permissions.Employee.Delete)]
        public IActionResult Delete(int id)
        {
            employeeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
