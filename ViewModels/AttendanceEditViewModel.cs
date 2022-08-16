namespace HRSystem.ViewModels
{
    public class AttendanceEditViewModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        [DataType(DataType.Date)]

        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [Remote("CheckStart", "Attendance", ErrorMessage = "Start time must be before end time.",AdditionalFields ="End")]
        public TimeSpan Start { get; set; }
        [DataType(DataType.Time)]
        [Remote("CheckEnd","Attendance",ErrorMessage = "End time must be after start time.", AdditionalFields = "Start")]
        public TimeSpan End { get; set; }
    }
}
