using HRSystem.Models;

namespace HRSystem.Repositories.SalaryRepo
{

    public class SalaryRepository : ISalaryRepository
    {
        private readonly HRDbContext context;
        private readonly IAttendanceRepository attendanceRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IGeneralSettingRepository generalSettingRepository;

        public SalaryRepository(HRDbContext context, IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository, IGeneralSettingRepository generalSettingRepository)
        {
            this.context = context;
            this.attendanceRepository = attendanceRepository;
            this.employeeRepository = employeeRepository;
            this.generalSettingRepository = generalSettingRepository;
        }
        public int countabsentdays(DateTime targetdate, int id)
        {
            int absentdays = context.Attendances.Where(x => x.EmpId == id).Include(x => x.Employee).Where(x => x.Date.Month == targetdate.Month).Count(x => x.Absent == true);
            return absentdays;
        }
        public int countOverTimeHours(DateTime targetdate, int id)
        {
            int overTimeHours = context.Attendances.Where(x => x.EmpId == id).Include(x => x.Employee).Where(x => x.Date.Month == targetdate.Month).Sum(x => x.BonusHours);
            return overTimeHours;
        }
        public int countDelayTimeHours(DateTime targetdate, int id)
        {
            int DelayTimeHours = context.Attendances.Where(x => x.EmpId == id).Include(x => x.Employee).Where(x => x.Date.Month == targetdate.Month).Sum(x => x.DiscountHours);
            return DelayTimeHours;
        }

        public int getExtaravlue()
        {

            var Extravalue = context.GeneralSettings.FirstOrDefault(x => x.ValueOfExtra != null);

            return Extravalue.ValueOfExtra;
        }

        public int getDeductionValue()
        {
            var Deductionvalue = context.GeneralSettings.FirstOrDefault(x => x.ValueOfDiscount != null);
            return Deductionvalue.ValueOfDiscount;
        }

        public int CalculatetotalOverTime(DateTime targetdate, int id)
        {
            int ExtraValue = context.GeneralSettings.FirstOrDefault(x => x.ValueOfExtra != null).ValueOfExtra;
            return countOverTimeHours(targetdate, id) * ExtraValue;

        }

        public List<SalaryWithAttend_Emp_GenSettingsVM> SalaryReport()
        {

            List<dateformual> RangDate = attendanceRepository.GetDateformuals();

            // Get All Employees from EmployeeModel
            List<Employee> EmployeeModel = employeeRepository.GetAllEmployee();
            //Get GeneralSettings---
            int OverTimePriceForHour = generalSettingRepository.OverTimePricePerHour();
            int DeductionPriceForHour = generalSettingRepository.DeductionPricePerHour();

            //Create List of ViewModel
            List<SalaryWithAttend_Emp_GenSettingsVM> salaryVM = new List<SalaryWithAttend_Emp_GenSettingsVM>();

            // Set ModelValue in ViewModel

            foreach (dateformual targetDate in RangDate)
            {
                foreach (Employee employee in EmployeeModel)
                {
                    //Get Attendance and absences ---> Mock Data will be Removed after Editing Attendance

                    //  Important ----->  Get Attendance Days ( Real Atttendance Days + Weekly Holidays )  .. then  we will  30 -attendance days  = absent days  
                    //int attendences = context.Attendances.Where(n => n.Absent != true && n.EmpId == emp.Id&&n.Date.Month==).ToList().Count;
                    int attendences = context.Attendances.Where(n => n.Absent != true && n.EmpId == employee.Id && n.Date.Month == targetDate.Month && n.Date.Year == targetDate.Year).ToList().Count;
                    //int absences = context.Attendances.Where(n => n.Absent == true && n.EmpId == employee.Id && n.Date.Month == targetDate.Month && n.Date.Year == targetDate.Year).ToList().Count;
                    // Get HolidaysperMonth 

                    int Holidays = (context.WeeklyHolidays.Select(x => x.Day).Count()) * 4;

                    int absences = 30 - (attendences + Holidays);

                    if (attendences != 0)
                    {
                        int NumberOfOverTimeHours = context.Attendances.Where(n => n.EmpId == employee.Id && n.BonusHours != 0 && n.Date.Month == targetDate.Month && n.Date.Year == targetDate.Year).Select(n => n.BonusHours).ToList().AsQueryable().Sum();
                        int NumebrOfLateHours = context.Attendances.Where(n => n.EmpId == employee.Id && n.DiscountHours != 0 && n.Date.Month == targetDate.Month && n.Date.Year == targetDate.Year).Select(n => n.DiscountHours).ToList().AsQueryable().Sum();
                        double SalaryPerDay = (employee.NetSalary) / 30;
                        double totalDeductionforAbsence = SalaryPerDay * absences;


                        salaryVM.Add(new SalaryWithAttend_Emp_GenSettingsVM
                        {
                            EmployeeIdVM = employee.Id,
                            EmployeeNameVM = employee.Name,
                            EmpolyeeContactNumberVM = employee.ContactNumber,
                            MonthSalaryBeforeOperations = employee.NetSalary,
                            AbsenceeDaysVM = absences,
                            AttendanceDaysVM = attendences,
                            DeductionPricePerHour = DeductionPriceForHour,
                            OverTimePricePerHour = OverTimePriceForHour,
                            TotalMoneyAdded = OverTimePriceForHour * NumberOfOverTimeHours,
                            TotalMoneyDeducted = DeductionPriceForHour * NumebrOfLateHours,
                            MonthSalaryAfterOperations = employee.NetSalary + (OverTimePriceForHour * NumberOfOverTimeHours) - (DeductionPriceForHour * NumebrOfLateHours) - (totalDeductionforAbsence),
                            filterdate = new dateformual { Year = targetDate.Year, Month = targetDate.Month }


                        });


                    }

                }



            }

            return salaryVM;
        }


        public List<SalaryWithAttend_Emp_GenSettingsVM> SalaryReport(int id, DateTime targetdate)
        {
            // Get All Employees from EmployeeModel
            Employee employee = employeeRepository.GetEmployeeById(id);

            // get absent 
            int attendences = context.Attendances.Where(n => n.Absent != true && n.EmpId == employee.Id && n.Date.Month == targetdate.Month && n.Date.Year == targetdate.Year).ToList().Count;

            // int  attendences = context.Attendances.Where(n => n.attend == true && n.EmpId == employee.Id && n.Date.Month == targetdate.Month && n.Date.Year == targetdate.Year).ToList().Count;
            //   int absentdays = context.Attendances.Where(n => n.Absent == true && n.EmpId == employee.Id && n.Date.Month == targetdate.Month && n.Date.Year == targetdate.Year).ToList().Count;
            int Holidays = (context.WeeklyHolidays.Select(x => x.Day).Count()) * 4;

            int absentdays = 30 - (attendences + Holidays);




            //get Over time in Money
            int overvalu = countOverTimeHours(targetdate, employee.Id) * getExtaravlue();
            //get delay time in Money
            int delayedvalu = countDelayTimeHours(targetdate, employee.Id) * getDeductionValue();
            //check absent day 


            double totalDeductionforAbsence = (employee.NetSalary / 30) * absentdays;


            int DeductionPriceForHour = getDeductionValue();
            var OverTimePriceForHour = getExtaravlue();
            double totalSalary = (employee.NetSalary + overvalu) - (delayedvalu + totalDeductionforAbsence);

            //Create List of ViewModel
            List<SalaryWithAttend_Emp_GenSettingsVM> salaryVM = new List<SalaryWithAttend_Emp_GenSettingsVM>();
            salaryVM.Add(new()
            {
                EmployeeIdVM = employee.Id,
                EmployeeNameVM = employee.Name,
                EmpolyeeContactNumberVM = employee.ContactNumber,
                MonthSalaryBeforeOperations = employee.NetSalary,
                AbsenceeDaysVM = absentdays,
                AttendanceDaysVM = attendences,
                DeductionPricePerHour = DeductionPriceForHour,
                OverTimePricePerHour = OverTimePriceForHour,
                TotalMoneyAdded = overvalu,
                TotalMoneyDeducted = delayedvalu,
                MonthSalaryAfterOperations = (employee.NetSalary + overvalu) - (delayedvalu + totalDeductionforAbsence),
                filterdate = new dateformual { Year = targetdate.Year, Month = targetdate.Month }
            });
            return salaryVM;
        }

        public SalaryWithAttend_Emp_GenSettingsVM EmpolyeeSalaryReport(int id, dateformual targetdate)
        {
            // Get All Employees from EmployeeModel

            Employee employee = employeeRepository.GetEmployeeById(id);

            //Get GeneralSettings
            int OverTimePriceForHour = generalSettingRepository.OverTimePricePerHour();
            int DeductionPriceForHour = generalSettingRepository.DeductionPricePerHour();

            //Get Attendance and absences 
            //  int absences = context.Attendances.Where(n => n.Absent == true && n.EmpId == employee.Id && n.Date.Month == targetdate.Month && n.Date.Year == targetdate.Year).ToList().Count;
            int attendences = context.Attendances.Where(n => n.Absent != true && n.EmpId == employee.Id && n.Date.Month == targetdate.Month && n.Date.Year == targetdate.Year).ToList().Count;
            int Holidays = (context.WeeklyHolidays.Select(x => x.Day).Count()) * 4;

            int absences = 30 - (attendences + Holidays);


            //Get Number Of OverTimes and LateHours
            int NumberOfOverTimeHours = context.Attendances.Where(n => n.EmpId == employee.Id && n.BonusHours != 0 && n.Date.Month == targetdate.Month && n.Date.Year == targetdate.Year).Select(n => n.BonusHours).ToList().AsQueryable().Sum();
            int NumebrOfLateHours = context.Attendances.Where(n => n.EmpId == employee.Id && n.DiscountHours != 0 && n.Date.Month == targetdate.Month && n.Date.Year == targetdate.Year).Select(n => n.DiscountHours).ToList().AsQueryable().Sum();
            double SalaryPerDay = (employee.NetSalary) / 30;
            double totalDeductionforAbsence = SalaryPerDay * absences;

            SalaryWithAttend_Emp_GenSettingsVM salaryVM = new SalaryWithAttend_Emp_GenSettingsVM
            {
                EmployeeIdVM = employee.Id,
                EmployeeNameVM = employee.Name,
                EmpolyeeContactNumberVM = employee.ContactNumber,
                MonthSalaryBeforeOperations = employee.NetSalary,
                AbsenceeDaysVM = absences,
                AttendanceDaysVM = attendences,
                DeductionPricePerHour = DeductionPriceForHour,
                OverTimePricePerHour = OverTimePriceForHour,
                TotalMoneyAdded = OverTimePriceForHour * NumberOfOverTimeHours,
                TotalMoneyDeducted = DeductionPriceForHour * NumebrOfLateHours,
                MonthSalaryAfterOperations = employee.NetSalary + (OverTimePriceForHour * NumberOfOverTimeHours) - (DeductionPriceForHour * NumebrOfLateHours) - (totalDeductionforAbsence)

            };
            return salaryVM;
        }


    }
}

