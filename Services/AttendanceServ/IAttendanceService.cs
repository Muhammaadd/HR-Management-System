using HRSystem.Repositories.AttendanceRepo;

namespace HRSystem.Services.AttendanceServ
{
    public interface IAttendanceService : IAttendanceRepository
    {
        List<EmployeeAttendanceViewModel> GetEmployeeAttendances();
        List<AttendanceExcelViewModel> ReadDataFromExcelSheet(string FileName);
        List<int> AddAttendanceToDatabase(List<AttendanceExcelViewModel> AttendanceData);
        void SaveChangesToDatabase(List<Attendance> attendances);
        int GetDiscount(TimeSpan AttendanceStart, TimeSpan EmployeeStart);
        int GetBonus(TimeSpan AttendanceEnd, TimeSpan EmployeeEnd);
        void UpdateAttendanceViewModel(AttendanceEditViewModel UpdatedAttendance, int Id);
        List<string> GetExtensions();
    }
}
