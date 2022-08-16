using HRSystem.Repositories.EmployeeRepo;

namespace HRSystem.Services.EmployeeServ
{
    public interface IEmployeeService : IEmployeeRepository
    {
        List<Department> GetAllDepartment();
        void InsertViewModel(EmployeeViewModel employeeViewModel);
        EmployeeViewModel GetViewModel(int id);
        void  UpdateEmployeeWithViewModel(EmployeeViewModel addEmployeeViewModel);
       
    }
}
