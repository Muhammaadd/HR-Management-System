namespace HRSystem.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }  
        [Required]
        [MinLength(8,ErrorMessage ="Enter the full name")]
        public string Name { get;set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "invalid email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "Password must contain 8 or more characters and at least one number, and one uppercase and lowercase letter")]
        public string PassWord { get; set; }
        public string GroupName { get; set; }
    }
}
