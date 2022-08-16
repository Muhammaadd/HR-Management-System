namespace HRSystem.ViewModels
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Minimum length is 10")]
        [RegularExpression("^((?!^Name$)[a-zA-Z '])+$", ErrorMessage = "Name must be properly formatted.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email must be properly formatted.")]
        [UniqueEmail]
        public string Email { get; set; }
        [Required]
        [MinLength(14, ErrorMessage = "Invalid National ID")]
        [MaxLength(14, ErrorMessage = "Invalid National ID")]
        [RegularExpression("[0-9]{14}", ErrorMessage = "Invalid National ID")]
        [UniqueSSN]
        public string NationalId { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Minimum length is 10")]
        public string Address { get; set; }
        [BirthDateCheck]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Range(5000,30000,ErrorMessage ="Salary Must Be Between (5K,30K) ")]
        public double NetSalary { get; set; }
        public string Nationality { get; set; }
        [RegularExpression("01[0125]{1}[0-9]{8}", ErrorMessage = "Invalid Contact Number")]
        [UniquePhoneNumber]
        public string ContactNumber { get; set; }
        [Required]
        public string Gender { get; set; }
       
        [Required]
        [CheckStartAndEnd(Type ='S')]
        [DataType(DataType.Time)]

        public TimeSpan Start { get; set; }
        [Required]
        [CheckStartAndEnd]
        [DataType(DataType.Time)]

        public TimeSpan End { get; set; }
        [Required]
        [HireDateCheck]
        [DataType(DataType.Date)]

        public DateTime ContractDate { get; set; }
        
        [Required]
        public int DeptId { get; set; }
    }
}
