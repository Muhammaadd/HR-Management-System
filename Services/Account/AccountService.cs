namespace HRSystem.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Hr> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly HRDbContext context;

        public AccountService(UserManager<Hr> userManager, RoleManager<IdentityRole> roleManager, HRDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public Hr GetById(string id)
        {
            return context.Users.FirstOrDefault(n => n.Id == id);
        }
        public void Delete(string id)
        {
            context.Users.Remove(GetById(id));
            context.SaveChanges();
        }
        public async Task<IdentityResult> AddUser(UserViewModel userViewModel)
        {
            Hr user = new Hr
            {
                Name = userViewModel.Name,
                UserName = userViewModel.Email,
                Email = userViewModel.Email
            };
            return await userManager.CreateAsync(user, userViewModel.PassWord);
        }

        public async Task<bool> CheckPassword(Hr hr, string password)
        {

            return await userManager.CheckPasswordAsync(hr, password);
        }

        public async Task<IdentityResult> Create(RegisterViewModel registerView)
        {
            Hr user = new Hr()
            {
                Name = registerView.Name,
                UserName = registerView.UserName,
                Email = registerView.Email,
                PhoneNumber = registerView.PhoneNumber,
            };
            return await userManager.CreateAsync(user, registerView.Password);
        }

        public async Task<List<UserDataViewModel>> GetAllUsers()
        {
            string GroupName;
            List<Hr> Users = context.Users.ToList();
            List<UserDataViewModel> usersModel = new List<UserDataViewModel>();
            foreach (Hr user in Users)
            {
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Count != 0)
                    GroupName = roles[0];
                else
                    GroupName = "";
                usersModel.Add(new UserDataViewModel { Id = user.Id, Name = user.Name, Email = user.Email, GroupName = GroupName });
            }
            return usersModel;
        }
        public List<UsersChatViewModel> GetAllUsersNames()
        {
            List<UsersChatViewModel> users = context.Users.Select(n => new UsersChatViewModel { Id = n.Id, Name = n.Name, UserName = n.UserName }).ToList();
            return users;
        }

        public async Task<Hr> GetByEmail(string email)
        {
            Hr user = await userManager.FindByEmailAsync(email);
            return user;
        }

        public Hr GetByPhone(string Phone)
        {
            return context.Users.FirstOrDefault(n => n.PhoneNumber == Phone);
        }
        public void Update(UserViewModel viewModel)
        {
            Hr oldUser = context.Users.FirstOrDefault(n => n.Id == viewModel.Id);
            oldUser.Name = viewModel.Name;
            oldUser.Email = viewModel.Email;
            context.SaveChanges();
            //IList<string> userRoles = await userManager.GetRolesAsync(oldUser);
            //await userManager.AddToRoleAsync(oldUser, viewModel.GroupName);
            //await userManager.RemoveFromRolesAsync(oldUser, userRoles);
            //string GroupName  =  userManager.GetRolesAsync(oldUser).Result[0];
            //await userManager.RemoveFromRoleAsync(oldUser, GroupName);

        }
    }
}
