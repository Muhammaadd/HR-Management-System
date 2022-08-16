namespace HRSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "minimum length is 10")]
        public string Name { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "minimum length is 3")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "invalid email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "Password must contain 8 or more characters and at least one number, and one uppercase and lowercase letter")]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$",ErrorMessage ="week password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        //[RegularExpression(@"/01210949994/gi", ErrorMessage ="invalid phone number")]
        public string PhoneNumber { get; set; }
    }
}
