using System.ComponentModel.DataAnnotations;

namespace HRSystem.Attributes
{
    public class CheckValidDate:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            HRDbContext dBContext = new HRDbContext();
            ExceptionAttendance exception = validationContext.ObjectInstance as ExceptionAttendance;


            if ((DateTime)value < DateTime.Now.AddDays(-1))
                return new ValidationResult("Add Valid Execption Date ");

            return ValidationResult.Success;

        }
    }
}
