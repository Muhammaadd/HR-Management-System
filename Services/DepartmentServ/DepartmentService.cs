using HRSystem.Repositories.DepartmentRepo;

namespace HRSystem.Services.DepartmentServ
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository DepartmentRepo;
        public DepartmentService(IDepartmentRepository DepartmentRepo)
        {
            this.DepartmentRepo = DepartmentRepo;
        }

        public void Add(DepartmentViewModel viewModel)
        {
            DepartmentRepo.Add(viewModel);
        }

        public void Delete(int id)
        {
            DepartmentRepo.Delete(id);
        }

        public void Edit(DepartmentViewModel department)
        {
            DepartmentRepo.Edit(department);
        }

        public List<Department> GetAll()
        {
            return DepartmentRepo.GetAll();
        }

        public Department GetById(int id)
        {
            return DepartmentRepo.GetById(id);
        }

        public Department GetByName(string Name)
        {
            return DepartmentRepo.GetByName(Name);
        }
    }
}
