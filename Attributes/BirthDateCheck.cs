namespace HRSystem.Attributes

{
    public class BirthDateCheck: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            EmployeeViewModel employee = validationContext.ObjectInstance as EmployeeViewModel;
            DateTime BirthDate = Convert.ToDateTime(value);
            DateTime MinAge = DateTime.Now.AddYears(-20);
            
            int x = DateTime.Compare(BirthDate, employee.ContractDate);
            int y = DateTime.Compare(BirthDate, MinAge);
            if (x == 0 || x > 0 || y > 0 )
                return new ValidationResult("Minumim Age Of Employee Must be 20 Or Greater");
            return ValidationResult.Success;
        }
    }
}
