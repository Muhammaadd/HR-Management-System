namespace HRSystem.Repositories.GroupRepo
{
    public class GroupRepository : IGroupRepository
    {
        private readonly HRDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;

        public GroupRepository(HRDbContext context,RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager;
        }

        public async Task<IdentityRole> GetById(string RoleId)
        {
            IdentityRole role = await roleManager.FindByIdAsync(RoleId);
            return role;

        }
        public void Delete(IdentityRole role)
        {
            context.Roles.Remove(role);
            context.SaveChanges();
        }

        public List<IdentityRole> GetRoles()
        {
            return context.Roles.ToList();
        }
    }
}
