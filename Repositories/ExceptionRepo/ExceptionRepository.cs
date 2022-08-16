namespace HRSystem.Repositories.ExceptionRepo
{
    public class ExceptionRepository : IExceptionRepository
    {
        private readonly HRDbContext context;
        public ExceptionRepository(HRDbContext context)
        {
            this.context = context;
        }
        public void Insert(ExceptionAttendance exception)
        {
            context.Exceptions.Add(exception);
            context.SaveChanges();
        }
        public ExceptionAttendance GetEmployeeException(int EmpId, DateTime AttendanceDate)
        {
            return context.Exceptions.Where(e => e.EmployeeId == EmpId && e.Date == AttendanceDate).FirstOrDefault();
        }
    }

}
