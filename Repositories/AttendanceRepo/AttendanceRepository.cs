using HRSystem.Models;

namespace HRSystem.Repositories.AttendanceRepo
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly HRDbContext context;
        public AttendanceRepository(HRDbContext context)
        {
            this.context = context;
        }
        public List<dateformual> GetDateformuals()
        {
            return context.Attendances.Select(x => new dateformual { Month = x.Date.Month, Year = x.Date.Year }).Distinct().ToList();
        }
        public List<Attendance> GetAll()
        {
            return context.Attendances.Include(n => n.Employee).ThenInclude(n=>n.Department).ToList();
        }
        public Attendance GetById(int Id)
        {
            return context.Attendances.Include(a => a.Employee).FirstOrDefault(a => a.Id == Id);
        }
        public void AddAttendance(Attendance NewAttendance)
        {
            context.Attendances.Add(NewAttendance);
            context.SaveChanges();
        }
        public void UpdateAttendance(Attendance UpdatedAttendance, int Id)
        {
            Attendance attendance = GetById(Id);
            if(attendance!=null)
            {
                attendance.Start = UpdatedAttendance.Start;
                attendance.End = UpdatedAttendance.End;
                attendance.Absent = UpdatedAttendance.Absent;
                attendance.BonusHours = UpdatedAttendance.BonusHours;
                attendance.DiscountHours = UpdatedAttendance.DiscountHours;
                context.SaveChanges();
            }
            else
            {
                AddAttendance(UpdatedAttendance);
            }


        }
        public int? GetAttendanceOfDate(int id , DateTime Date)
        {
            int? SerachAttendanceId = context.Attendances.Where(a => a.EmpId == id && a.Date == Date).Select(a=>a.Id).FirstOrDefault();
            return SerachAttendanceId;
        }
        public void DeleteAttendance(int id)
        {
            context.Attendances.Remove(GetById(id));
            context.SaveChanges();
        }
        public List<EmployeeAttendanceViewModel> Search(SearchAttendanceViewModel viewModel)
        {
        

            List<Attendance> attendances = context.Attendances.Include(e => e.Employee).ThenInclude(d=>d.Department).ToList();
            if(viewModel.Name==null && viewModel.StartDate == null && viewModel.EndDate == null)
            {
                return MappingAttendanceToEmpAttedVM(attendances);
            }
         
            if (viewModel.Name == null && viewModel.StartDate != null && viewModel.EndDate != null)
            {
                string startdate = viewModel.StartDate.Value.ToString("dd/MM/yyyy");
                viewModel.StartDate = DateTime.Parse(startdate);
                string enddate = viewModel.EndDate.Value.ToString("dd/MM/yyyy");
                viewModel.EndDate = DateTime.Parse(enddate);
                return MappingAttendanceToEmpAttedVM(attendances.Where(n => n.Date >= viewModel.StartDate && n.Date <= viewModel.EndDate).ToList());
            }
           
            if (viewModel.StartDate == null && viewModel.EndDate == null)
            {
                List<Attendance> allEmployeeAttendace = attendances
               .Where(n=>n.Employee.Name.ToLower().Contains(viewModel.Name.ToLower())).ToList();
                if (allEmployeeAttendace.Count !=0)
                    return MappingAttendanceToEmpAttedVM(allEmployeeAttendace);
                
               
                List<Attendance> attendancesByDept = context.Attendances.Include(n => n.Employee).
                ThenInclude(n => n.Department)
               .Where(n=>n.Employee.Department.Name.ToLower().Contains(viewModel.Name.ToLower())).ToList();
                     return MappingAttendanceToEmpAttedVM(attendancesByDept);

            }
            string startdate1 = viewModel.StartDate.Value.ToString("dd/MM/yyyy");
            viewModel.StartDate = DateTime.Parse(startdate1);
            string enddate1 = viewModel.EndDate.Value.ToString("dd/MM/yyyy");
            viewModel.EndDate = DateTime.Parse(enddate1);

            List<Attendance> allEmployeeAttendace1 = attendances
               .Where(n => (n.Date >= viewModel.StartDate && n.Date <= viewModel.EndDate) && n.Employee.Name.ToLower().Contains(viewModel.Name.ToLower())).ToList();
            if (allEmployeeAttendace1.Count != 0)
                return MappingAttendanceToEmpAttedVM(allEmployeeAttendace1);


            List<Attendance> attendancesByDept1 = context.Attendances.Include(n => n.Employee).
            ThenInclude(n => n.Department)
           .Where(n =>(n.Date >= viewModel.StartDate && n.Date <= viewModel.EndDate) && n.Employee.Department.Name.ToLower().Contains(viewModel.Name.ToLower())).ToList();
            return MappingAttendanceToEmpAttedVM(attendancesByDept1);

            

            


           
        }
        public List<EmployeeAttendanceViewModel> MappingAttendanceToEmpAttedVM(List<Attendance> attendances)
        {
            List<EmployeeAttendanceViewModel> employeeAttendanceViewModels = new List<EmployeeAttendanceViewModel>();
            foreach (Attendance attendance  in attendances)
            {
                employeeAttendanceViewModels.Add(new EmployeeAttendanceViewModel { 
                    AttendanceId = attendance.Id,
                    CheckInTime = attendance.Start,
                    Date=attendance.Date,
                    CheckOutTime = attendance.End,
                    EmployeeName = attendance.Employee.Name,
                    DepartmentName =attendance.Employee.Department.Name
                });
            }
            return employeeAttendanceViewModels;
        }
    }
}
