namespace HRSystem.Services.Account
{
    public interface IAccountService
    {
        Task<List<UserDataViewModel>> GetAllUsers();
        Task<Hr> GetByEmail(string email);
        Hr GetByPhone(string Phone);
        Task<IdentityResult> Create(RegisterViewModel registerView);
        Task<IdentityResult> AddUser(UserViewModel userViewModel);
        Task<bool> CheckPassword(Hr hr, string password);
        Hr GetById(string id);
        void Delete(string id);
        void Update(UserViewModel viewModel);
        List<UsersChatViewModel> GetAllUsersNames();

    }
}
