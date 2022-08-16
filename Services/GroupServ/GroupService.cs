namespace HRSystem.Services.GroupServ
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public void Delete(IdentityRole Role)
        {
            groupRepository.Delete(Role);
        }

        public Task<IdentityRole> GetById(string RoleId)
        {
            return groupRepository.GetById(RoleId);
        }

        public List<IdentityRole> GetRoles()
        {
            return groupRepository.GetRoles();
        }
    }
}
