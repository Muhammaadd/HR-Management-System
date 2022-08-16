namespace HRSystem.ViewModels
{
    public class EmployeeAttendanceViewModel
    {
        public int AttendanceId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }

        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public DateTime Date { get; set; }
    }
}
