namespace HRSystem.Attributes

{
    public class HireDateCheck:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            EmployeeViewModel employee = validationContext.ObjectInstance as EmployeeViewModel;
            DateTime hireDate = Convert.ToDateTime(value);
            int x = DateTime.Compare(hireDate,employee.BirthDate);
            int y = DateTime.Compare(hireDate, DateTime.Now);
            DateTime companyStart = new DateTime(2018, 01, 01);
            int z = DateTime.Compare(hireDate,companyStart);

            if (x==0||x < 0||y>0||z<0)
                return new ValidationResult("Invalid Date");
            return ValidationResult.Success;
        }
    }
}
