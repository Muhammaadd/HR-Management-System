using Microsoft.AspNetCore.Mvc;
namespace HRSystem.ViewModels
{
    public class PermissionsFormViewModel
    {
        public string? RoleId { get; set; }
        [Required]
        [Remote("UniqueGroupName","Group",AdditionalFields = "RoleId", ErrorMessage ="This Group name already exist")]
        public string RoleName { get; set; }
        public List<CheckBoxViewModel> RoleCalims { get; set; }
    }
}
