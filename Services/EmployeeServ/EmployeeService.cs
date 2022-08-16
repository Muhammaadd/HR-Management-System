using HRSystem.Repositories.EmployeeRepo;

namespace HRSystem.Services.EmployeeServ
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository EmployeeRepo;
        private readonly IDepartmentRepository departmentRepository;

        public EmployeeService(IEmployeeRepository EmployeeRepo,IDepartmentRepository departmentRepository)
        {
            this.EmployeeRepo = EmployeeRepo;
            this.departmentRepository = departmentRepository;
        }
        public List<Employee> GetAllEmployee()
        {
           return EmployeeRepo.GetAllEmployee();
        }

        public Employee GetEmployeeById(int id)
        {
            return  EmployeeRepo.GetEmployeeById(id);
        }

        public  List<Employee> GetEmployeeByName(string name)
        {
            return EmployeeRepo.GetEmployeeByName(name);
        }

        public void Insert(Employee employee)
        {
            EmployeeRepo.Insert(employee);
        }

        public void Update(Employee employee)
        {
            EmployeeRepo.Update(employee);
        }
        public void Delete(int id)
        {
            EmployeeRepo.Delete(id);
        }

        public void InsertViewModel(EmployeeViewModel employeeViewModel)
        {

            Employee employee = new Employee()
            {
                Address = employeeViewModel.Address,
                BirthDate = employeeViewModel.BirthDate,
                ContactNumber = employeeViewModel.ContactNumber,
                ContractDate = employeeViewModel.ContractDate,
                DeptId = employeeViewModel.DeptId,
                Email = employeeViewModel.Email,
                End = employeeViewModel.End,
                Name = employeeViewModel.Name,
                NationalId = employeeViewModel.NationalId,
                Nationality = employeeViewModel.Nationality,
                Gender = employeeViewModel.Gender,
                NetSalary = employeeViewModel.NetSalary,
                Start = employeeViewModel.Start
            };
            EmployeeRepo.Insert(employee);            
        }
        public EmployeeViewModel GetViewModel(int id)
        {
            Employee employee = EmployeeRepo.GetEmployeeById(id);

            EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            {
                Address = employee.Address,
                BirthDate = employee.BirthDate,
                ContactNumber = employee.ContactNumber,
                ContractDate = employee.ContractDate,
                DeptId = (int)employee.DeptId,
                Email = employee.Email,
                End = employee.End,
                Name = employee.Name,
                NationalId = employee.NationalId,
                Nationality = employee.Nationality,
                Gender = employee.Gender,
                NetSalary = employee.NetSalary,
                Start = employee.Start
            };
            return employeeViewModel;
        }

        public void UpdateEmployeeWithViewModel(EmployeeViewModel EmployeeViewModel)
        {
            Employee employee = new Employee()
            {
                Id= (int)EmployeeViewModel.Id,
                Name = EmployeeViewModel.Name,
                Address = EmployeeViewModel.Address,
                BirthDate= EmployeeViewModel.BirthDate,
                ContractDate= EmployeeViewModel.ContractDate,
                ContactNumber= EmployeeViewModel.ContactNumber,
                DeptId=EmployeeViewModel.DeptId,
                Email=EmployeeViewModel.Email,
                Start=EmployeeViewModel.Start,
                End=EmployeeViewModel.End,
                Gender=EmployeeViewModel.Gender,
                NationalId=EmployeeViewModel.NationalId,
                Nationality=EmployeeViewModel.Nationality,
                NetSalary=EmployeeViewModel.NetSalary
            };
            EmployeeRepo.Update(employee);
            
        }

        public Employee GetEmployeeByNationalId(string Id)
        {
           return EmployeeRepo.GetEmployeeByNationalId(Id);
        }

        public List<Department> GetAllDepartment()
        {
            return departmentRepository.GetAll();
        }
    }
}
