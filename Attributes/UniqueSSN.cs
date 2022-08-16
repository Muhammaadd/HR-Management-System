using Microsoft.EntityFrameworkCore;

namespace HRSystem.Attributes

{
    public class UniqueSSN:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            HRDbContext dBContext = new HRDbContext();
           
            Employee employee = dBContext.Employees.Where(n => n.NationalId == (string)value).FirstOrDefault();
            EmployeeViewModel currentEmployee = validationContext.ObjectInstance as EmployeeViewModel;
            if (employee != null && employee.Id != currentEmployee.Id)
            {
                return new ValidationResult("This National ID is already exist");
            }
            return ValidationResult.Success;
        }

    }
}

