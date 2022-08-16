using HRSystem.Repositories.ExceptionRepo;

namespace HRSystem.Services.ExceptionServ
{
    public class ExceptionService : IExceptionService
    {
        private readonly IExceptionRepository ExceptionRepo;

        public ExceptionService(IExceptionRepository ExceptionRepo)
        {
            this.ExceptionRepo = ExceptionRepo;
        } 
        public void Insert(ExceptionAttendance exception)
        {
            ExceptionRepo.Insert(exception);
        }
        public ExceptionAttendance GetEmployeeException(int EmployeeId,DateTime AttendanceDate)
        {
            return ExceptionRepo.GetEmployeeException(EmployeeId, AttendanceDate);
        }
    }
}
