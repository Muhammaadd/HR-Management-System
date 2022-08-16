using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        [Authorize(Permissions.Department.View)]

        public IActionResult Index()
        {
            return View(departmentService.GetAll());
        }
        [HttpGet]
        [Authorize(Permissions.Department.Create)]

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Permissions.Department.Create)]

        public IActionResult Add(DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            departmentService.Add(viewModel);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Permissions.Department.Edit)]

        public IActionResult Edit(int Id)
        {
            Department department = departmentService.GetById(Id);
            DepartmentViewModel viewModel = new DepartmentViewModel { Id = department.Id, Name = department.Name };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(DepartmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            departmentService.Edit(viewModel);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Permissions.Department.Delete)]

        public IActionResult Delete(int Id)
        {
            departmentService.Delete(Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Unique(string Name, int Id)
        {
            Department department = departmentService.GetByName(Name);
            if (department == null || department.Id == Id)
                return Json(true);
            return Json(false);
        }
    }
}
