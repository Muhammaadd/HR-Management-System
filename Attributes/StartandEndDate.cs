namespace HRSystem.Attributes
{
    public class StartDate:ValidationAttribute
    {
        
            protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
            {
                SearchAttendanceViewModel searchAttendance = validationContext.ObjectInstance as SearchAttendanceViewModel;

            if (value == null)
                return new ValidationResult("This Filed Is Required ");
            return ValidationResult.Success;
        }
    }
    public class EndDate : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            SearchAttendanceViewModel searchAttendance = validationContext.ObjectInstance as SearchAttendanceViewModel;

            

            if (searchAttendance.StartDate > searchAttendance.EndDate)
                return new ValidationResult("InValid Date");
            return ValidationResult.Success;
        }
    }
}

