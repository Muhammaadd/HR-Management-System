namespace HRSystem.ViewModels
{
    public class GeneralSettingViewModel
    {
        [Required(ErrorMessage = "This Field is required.")]
        [Range(10, 200, ErrorMessage = "The extra hour between 10$ and 200$.")]
        [Display(Name = "The Extra:")]
        public int Extra { get; set; }
        [Required(ErrorMessage = "This Field is required.")]
        [Range(10, 200, ErrorMessage = "The extra hour between 10$ and 200$.")]
        [Display(Name = "Discount:")]
        public int Discount { get; set; }
        [DaysChecked]
        public List<DaysWithChecked> DaysChecked { get; set; }
    }
    public class DaysWithChecked
    {
        public string Day { get; set; }
        public Boolean Checked { get; set; }
    }
}
