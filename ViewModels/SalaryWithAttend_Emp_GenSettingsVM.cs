namespace HRSystem.ViewModels
{
    // Creating ViewModel To Set Specific Data from Employee ,  Attendance and General Settings
    public class SalaryWithAttend_Emp_GenSettingsVM
    {
        // From Employee
        public int EmployeeIdVM { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Minimum length is 10")]
        [RegularExpression("^((?!^Name$)[a-zA-Z '])+$", ErrorMessage = "Name must be properly formatted.")]

        public string EmployeeNameVM { get; set; }

        [RegularExpression("01[0125]{1}[0-9]{8}", ErrorMessage = "Invalid Contact Number")]
        [UniquePhoneNumber]
        public string? EmpolyeeContactNumberVM { get; set; }
        [Range(5000, 30000, ErrorMessage = "Salary Must Be Between (5K,30K) ")]
        public double MonthSalaryBeforeOperations { get; set; }

        // From Attendance 
        [Required]
        public int AttendanceDaysVM { get; set; }
        public int AbsenceeDaysVM { get; set; }


        //From Genral Settings 

        public double OverTimePricePerHour { get; set; }
        public double DeductionPricePerHour { get; set; }

        // Added Properties
        // TotalMoneyDeducted = DeductionPricePerHour * Number Of LateHours(from Attendance Table)
        public double TotalMoneyDeducted { get; set; }

        //totalMoneyAdded =  OverTimePricePerHour *  Number Of OverTimeHours(from Attendance Table ) 
        public double TotalMoneyAdded { get; set; }

        //MonthSalaryAfterOperations = NetSalary Before any Operations   + (TotalMoneyAdded )  - (TotalMoneyDeducted) 
        public double MonthSalaryAfterOperations { get; set; }


        // public DateTime filterdate { get; set; }
        public dateformual? filterdate { get; set; }

    }
    public class dateformual
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }

}