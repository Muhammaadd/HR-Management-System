namespace HRSystem.ViewModels
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string passwrod { get; set; }
        public bool RemeberMe { get; set; }
    }
}
