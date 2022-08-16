namespace HRSystem.Attributes
{
    public class CheckStartAndEnd: ValidationAttribute
    {
        public char Type { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            HRDbContext dBContext = new HRDbContext();
            EmployeeViewModel employee = validationContext.ObjectInstance as EmployeeViewModel;

            TimeSpan CompanyStart = new TimeSpan(12, 0, 0);
            TimeSpan CompanyEnd = new TimeSpan(20, 0, 0);


            if (Type == 'S')
            {
                //TimeSpan start;
                //TimeSpan end;
                //TimeSpan.TryParse(value.ToString(), out start);
                //TimeSpan.TryParse(employee.End.ToString(), out end);


                if ((TimeSpan)value > employee.End ||(TimeSpan)value > CompanyStart)
                {
                    return new ValidationResult("InValid  Start Time");
                }
                return ValidationResult.Success;

            }
            else { 
                //TimeSpan start;
                //TimeSpan end;
                //TimeSpan.TryParse((string)value, out end);
                //TimeSpan.TryParse(employee.End.ToString(), out start);

             if((TimeSpan)value > employee.End ||(TimeSpan)value > CompanyEnd)
                {
                    return new ValidationResult("InValid End Time");

                }
                return ValidationResult.Success;

            }
        }
    }
}
