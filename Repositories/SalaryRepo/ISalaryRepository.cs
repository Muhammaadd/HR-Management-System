namespace HRSystem.Repositories.SalaryRepo
{
    public interface ISalaryRepository
    {
        
            int countabsentdays(DateTime targetdate, int id);

            int countOverTimeHours(DateTime targetdate, int id);

            int countDelayTimeHours(DateTime targetdate, int id);

            int getExtaravlue();

            int getDeductionValue();

            int CalculatetotalOverTime(DateTime targetdate, int id);
            public List<SalaryWithAttend_Emp_GenSettingsVM> SalaryReport();
            public List<SalaryWithAttend_Emp_GenSettingsVM> SalaryReport(int id, DateTime targetdate);
            public SalaryWithAttend_Emp_GenSettingsVM EmpolyeeSalaryReport(int id, dateformual targetdate);
        
    }
}
