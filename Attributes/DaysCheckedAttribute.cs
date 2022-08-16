namespace HRSystem.Attributes
{
    public class DaysCheckedAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Cast value object to days with check list
            List<DaysWithChecked> CheckedList = value as List<DaysWithChecked>;
            // Loop in the List to check if any day was checked
            foreach (var item in CheckedList)
            {
                if (item.Checked)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("Check at least one day.");
        }
    }
}
