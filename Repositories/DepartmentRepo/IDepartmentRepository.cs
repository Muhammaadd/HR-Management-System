namespace HRSystem.Repositories.DepartmentRepo
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department GetById(int id);
        Department GetByName(string Name);
        void Delete(int id);
        void Edit(DepartmentViewModel viewModel);
        void Add(DepartmentViewModel viewModel);
    }
}
