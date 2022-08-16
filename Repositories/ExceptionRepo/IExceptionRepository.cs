namespace HRSystem.Repositories.ExceptionRepo
{
    public interface IExceptionRepository
    {
        void Insert(ExceptionAttendance exception);
        ExceptionAttendance GetEmployeeException(int EmpId, DateTime AttendanceDate);
    }
}
