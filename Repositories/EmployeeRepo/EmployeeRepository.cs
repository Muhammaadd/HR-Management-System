using HRSystem.Models;

namespace HRSystem.Repositories.EmployeeRepo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRDbContext context;
        public EmployeeRepository(HRDbContext context)
        {
            this.context = context;
        }
        public List<Employee> GetAllEmployee()
        {
            return context.Employees.ToList();
        }
        public Employee GetEmployeeById(int id)
        {
            return context.Employees.Include(e=>e.Department).FirstOrDefault(emp => emp.Id == id);
        }
        public List< Employee> GetEmployeeByName(string name)
        {
            return context.Employees.Where(emp => emp.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public void Insert(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }
        public void Update(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Employee oldEmployee = GetEmployeeById(id);
            context.Employees.Remove(oldEmployee);
            context.SaveChanges();
        }
        public Employee GetEmployeeByNationalId(string Id)
        {
            return context.Employees.FirstOrDefault(e => e.NationalId == Id);
        }

    }
}
