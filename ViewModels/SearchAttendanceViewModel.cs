namespace HRSystem.ViewModels
{
    public class SearchAttendanceViewModel
    {

        public string? Name { get; set; }
        [DataType(DataType.Date)]

        //  [Remote("CheckSearchDate","Attendance",AdditionalFields = "EndDate", ErrorMessage = "Invalid Start Date or End Date")]
        public DateTime? StartDate { get; set; }
        [DataType(DataType.Date)]
        [EndDate]

        public DateTime? EndDate { get; set; }
    }
}
