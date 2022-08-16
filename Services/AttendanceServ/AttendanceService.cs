using HRSystem.Models;
using HRSystem.Repositories.AttendanceRepo;
using System;

namespace HRSystem.Services.AttendanceServ
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository AttendanceRepo;
        private readonly IEmployeeService EmployeeService;
        private readonly IGeneralSettingService GeneralSetting;
        private readonly IExceptionService ExceptionService;

        public AttendanceService(IAttendanceRepository AttendanceRepo,IEmployeeService EmployeeService, IGeneralSettingService GeneralSetting, IExceptionService ExceptionService)
        {
            this.AttendanceRepo = AttendanceRepo;
            this.EmployeeService = EmployeeService;
            this.GeneralSetting = GeneralSetting;
            this.ExceptionService = ExceptionService;
        }
        public List<Attendance> GetAll()
        {
            return AttendanceRepo.GetAll();
        }
        public List<EmployeeAttendanceViewModel> GetEmployeeAttendances()
        {
            var EmployeeAttendances = new List<EmployeeAttendanceViewModel>();
            var Attendances = GetAll();
            foreach (var attendance in Attendances)
            {
                EmployeeAttendances.Add(new EmployeeAttendanceViewModel { AttendanceId = attendance.Id, EmployeeName = attendance.Employee.Name, CheckInTime = attendance.Start, CheckOutTime = attendance.End, Date = attendance.Date,DepartmentName=attendance.Employee.Department.Name });
            }
            return EmployeeAttendances;
        }
        public List<AttendanceExcelViewModel> ReadDataFromExcelSheet(string FileName)
        {
            List<AttendanceExcelViewModel> ExcelData = new List<AttendanceExcelViewModel>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(FileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each row of the file
                    {
                        if (reader.GetValue(0) == null)
                        {
                            break;
                        }
                        try
                        {
                            ExcelData.Add(new AttendanceExcelViewModel()
                            {
                                SSN = reader.GetValue(0).ToString().Trim(),
                                CheckInTime = reader.GetValue(1).ToString().Trim(),
                                CheckOutTime = reader.GetValue(2).ToString().Trim(),
                                Date = reader.GetValue(3).ToString().Trim()
                            });
                        }
                        catch (System.Exception ex)
                        {
                            throw new System.Exception("Excel Sheet Unformated Well.", ex);
                        }
                    }
                }
            }
            return ExcelData;
        }
        public List<int> AddAttendanceToDatabase(List<AttendanceExcelViewModel> AttendanceData)
        {
            AttendanceData.RemoveAt(0);
            List<int> UnformatedRows = new List<int>();
            List<Attendance> Attendances = new List<Attendance>();
            for (int i = 0; i < AttendanceData.Count; i++)
            {
                Employee employee = EmployeeService.GetEmployeeByNationalId(AttendanceData[i].SSN);
                if(employee == null)
                {
                    UnformatedRows.Add(i);
                    continue;
                }
                if (DateTime.Parse(AttendanceData[i].CheckInTime).TimeOfDay > DateTime.Parse(AttendanceData[i].CheckOutTime).TimeOfDay)
                {
                    UnformatedRows.Add(i);
                    continue;
                }
                if (DateTime.Compare(DateTime.Parse(AttendanceData[i].Date), DateTime.Now) > 0)
                {
                    UnformatedRows.Add(i);
                    continue;
                }
                Attendances.Add(new Attendance()
                {
                    EmpId = employee.Id,
                    Start = DateTime.Parse(AttendanceData[i].CheckInTime).TimeOfDay,
                    End = DateTime.Parse(AttendanceData[i].CheckOutTime).TimeOfDay,
                    Date = DateTime.Parse(AttendanceData[i].Date)
                }) ;
            }
            SaveChangesToDatabase(Attendances);
            return UnformatedRows;
        }
        public void SaveChangesToDatabase(List<Attendance> attendances)
        {
            int DiscountTime = 0;
            int BonusTime = 0;
            for (int i = 0; i < attendances.Count; i++)
            {
                Employee employee = EmployeeService.GetEmployeeById((int)attendances[i].EmpId);
                if (attendances[i].Start == attendances[i].End)
                {
                    attendances[i].Absent = true;
                }
                ExceptionAttendance ExceptionAttendance = CheckException(employee.Id, attendances[i].Date);
                if (ExceptionAttendance != null)
                {
                    DiscountTime = GetDiscount(attendances[i].Start, ExceptionAttendance.Start);
                    attendances[i].DiscountHours = DiscountTime;
                    BonusTime = GetBonus(attendances[i].End, ExceptionAttendance.End);
                    if(BonusTime > 0)
                    {
                        BonusTime = GetBonus(attendances[i].End, employee.End);
                        if(BonusTime >= 0)
                        {
                            attendances[i].BonusHours = BonusTime;
                        }
                        else
                        {
                            attendances[i].DiscountHours += -BonusTime;
                        }
                    }
                    else
                    {
                        attendances[i].DiscountHours += -BonusTime;
                    }
                }
                else
                {
                    DiscountTime = GetDiscount(attendances[i].Start, employee.Start);
                    attendances[i].DiscountHours = DiscountTime;
                    BonusTime = GetBonus(attendances[i].End, employee.End);
                    if(BonusTime >= 0)
                    {
                        attendances[i].BonusHours = BonusTime;
                    }
                    else
                    {
                        attendances[i].DiscountHours += -BonusTime;
                    }
                }
                int? AttendanceId = GetAttendanceOfDate(employee.Id, attendances[i].Date);
                if (AttendanceId != null)
                {
                    UpdateAttendance(attendances[i],(int)AttendanceId);
                }
                else
                {
                    AddAttendance(attendances[i]);
                }
            }
        }
        public int GetDiscount(TimeSpan AttendanceStart, TimeSpan EmployeeStart)
        {
            int DiscountTime = 0;
            if (AttendanceStart > EmployeeStart)
            {
                TimeSpan Difference = AttendanceStart - EmployeeStart;
                int DifferenceMinutes = AttendanceStart.Minutes;
                if (DifferenceMinutes > 15)
                {
                    DiscountTime = (int)Difference.TotalHours;
                    return DiscountTime + 1;
                }
                else
                {
                    DiscountTime = (int)Difference.TotalHours;
                    return DiscountTime;
                }
            }
            return DiscountTime;
        }
        public ExceptionAttendance CheckException(int EmployeeId, DateTime AttendanceDate)
        {
            return ExceptionService.GetEmployeeException(EmployeeId, AttendanceDate);
        }
        public int GetBonus(TimeSpan AttendanceEnd, TimeSpan EmployeeEnd)
        {
            int BonusTime = 0;
            if (AttendanceEnd > EmployeeEnd)
            {
                TimeSpan Difference = AttendanceEnd - EmployeeEnd;
                int DifferenceMinutes = AttendanceEnd.Minutes;
                if (DifferenceMinutes > 15)
                {
                    BonusTime = (int)Difference.TotalHours;
                    return BonusTime + 1;
                }
                else
                {
                    BonusTime = (int)Difference.TotalHours;
                    return BonusTime;
                }
            }
            else
            {
                TimeSpan Difference = EmployeeEnd - AttendanceEnd;
                int DifferenceMinutes = AttendanceEnd.Minutes;
                if (DifferenceMinutes > 15)
                {
                    BonusTime = (int)Difference.TotalHours;
                    return -(BonusTime + 1);
                }
                else
                {
                    BonusTime = (int)Difference.TotalHours;
                    return -BonusTime;
                }
            }
        }
        public void AddAttendance(Attendance NewAttendance)
        {
            AttendanceRepo.AddAttendance(NewAttendance);
        }
        public int? GetAttendanceOfDate(int id, DateTime Date)
        {
            return AttendanceRepo.GetAttendanceOfDate(id, Date);
        }
        public void UpdateAttendance(Attendance UpdatedAttendance, int Id)
        {
            AttendanceRepo.UpdateAttendance(UpdatedAttendance, Id);
        }
        public Attendance GetById(int Id)
        {
            return AttendanceRepo.GetById(Id);
        }
        public void DeleteAttendance(int id)
        {
            AttendanceRepo.DeleteAttendance(id);
        }
        public void UpdateAttendanceViewModel(AttendanceEditViewModel UpdatedAttendance, int Id)
        {
            Attendance Attendance = GetById(Id);
            Employee Employee = EmployeeService.GetEmployeeById((int)Attendance.EmpId);
            int DiscountTime = GetDiscount(UpdatedAttendance.Start, Employee.Start);
            Attendance.DiscountHours = DiscountTime;
            int BonusTime = GetBonus(UpdatedAttendance.End, Employee.End);
            Attendance.BonusHours = BonusTime;
            Attendance.Start = UpdatedAttendance.Start;
            Attendance.End = UpdatedAttendance.End;
            UpdateAttendance(Attendance, Id);
        }
        public List<string> GetExtensions()
        {
            return new List<string> { ".xlsx" , ".xls"};
        }
        public List<EmployeeAttendanceViewModel> Search(SearchAttendanceViewModel viewModel)
        {
            return AttendanceRepo.Search(viewModel);
        }

        public List<dateformual> GetDateformuals()
        {
            return AttendanceRepo.GetDateformuals();
        }

    }
}
