namespace HRSystem.Attributes

{
    public class UniqueEmail:ValidationAttribute
    {
       
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            HRDbContext dBContext = new HRDbContext();
            Employee employee = dBContext.Employees.Where(n => n.Email == (string)value).FirstOrDefault();
            EmployeeViewModel currentEmployee = validationContext.ObjectInstance as EmployeeViewModel;
            if (employee != null && employee.Id != currentEmployee.Id)
            {
                return new ValidationResult("This email already exists");
            }
            return ValidationResult.Success;
        }
    }
}
