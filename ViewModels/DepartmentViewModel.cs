namespace HRSystem.ViewModels
{
    public class DepartmentViewModel
    {
        public int? Id { get; set; }
        [Remote("Unique","Department",AdditionalFields ="Id",ErrorMessage ="This name already exist")]
        public string Name { get; set; }
    }
}
