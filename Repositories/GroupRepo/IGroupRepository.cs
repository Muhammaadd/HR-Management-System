namespace HRSystem.Repositories.GroupRepo
{
    public interface IGroupRepository
    {
        List<IdentityRole> GetRoles();
        void Delete(IdentityRole role);
        Task<IdentityRole> GetById(string RoleId);
    }
}
