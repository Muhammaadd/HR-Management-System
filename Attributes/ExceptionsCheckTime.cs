namespace HRSystem.Attributes
{
    public class ExceptionsCheckTime:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            HRDbContext dBContext = new HRDbContext();
            ExceptionAttendance exception = validationContext.ObjectInstance as ExceptionAttendance;


            if(value == null)
                return new ValidationResult("InValid Start Time");
            if(exception.Start > exception.End)
                return new ValidationResult("InValid Time");

            return ValidationResult.Success;

            }

        }
    }
