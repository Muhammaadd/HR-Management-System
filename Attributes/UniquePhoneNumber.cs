namespace HRSystem.Attributes

{
    public class UniquePhoneNumber:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            HRDbContext dBContext = new HRDbContext();
            Employee employee = dBContext.Employees.Where(n => n.ContactNumber == (string)value).FirstOrDefault();
            EmployeeViewModel currentEmployee = validationContext.ObjectInstance as EmployeeViewModel;
            if (employee != null && employee.Id != currentEmployee.Id)
            {
                return new ValidationResult("This Phone number already used");
            }
            return ValidationResult.Success;
        }
    }
}
