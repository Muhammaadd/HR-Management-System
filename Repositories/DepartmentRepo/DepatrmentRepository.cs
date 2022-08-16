using HRSystem.Models;

namespace HRSystem.Repositories.DepartmentRepo
{
    public class DepatrmentRepository : IDepartmentRepository
    {
        private readonly HRDbContext context;
        public DepatrmentRepository(HRDbContext context)
        {
            this.context = context;
        }

        public void Add(DepartmentViewModel viewModel)
        {
            Department department = new Department { Name = viewModel.Name };
            context.Departments.Add(department);
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            context.Departments.Remove(GetById(id));
            context.SaveChanges();
        }

        public void Edit(DepartmentViewModel viewModel)
        {
            Department department = GetById((int)viewModel.Id);
            department.Name = viewModel.Name;
            context.SaveChanges();

        }
            
        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return context.Departments.FirstOrDefault(n => n.Id == id);
        }

        public Department GetByName(string Name)
        {
            return context.Departments.FirstOrDefault(n => n.Name == Name);
        }
    }
}
