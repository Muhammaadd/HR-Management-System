namespace HRSystem.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployee();
        Employee GetEmployeeById(int id);
        List<Employee> GetEmployeeByName(string name);
        void Insert(Employee employee);
        void Update(Employee employee);
        void Delete(int id);
        Employee GetEmployeeByNationalId(string Id);

    }
}
