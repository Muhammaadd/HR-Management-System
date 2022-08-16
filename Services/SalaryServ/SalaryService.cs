using HRSystem.Repositories.SalaryRepo;

namespace HRSystem.Services.SalaryServ
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository SalaryRepo;


        public SalaryService(ISalaryRepository SalaryRepo)
        {
            this.SalaryRepo = SalaryRepo;

        }



        public int countabsentdays(DateTime targetdate, int id)
        {
            return SalaryRepo.countabsentdays(targetdate, id);
        }

        public int countDelayTimeHours(DateTime targetdate, int id)
        {
            return SalaryRepo.countDelayTimeHours(targetdate, id);
        }

        public int countOverTimeHours(DateTime targetdate, int id)
        {
            return SalaryRepo.countOverTimeHours(targetdate, id);
        }

        public int getDeductionValue()
        {
            return SalaryRepo.getDeductionValue();
        }


        public int getExtaravlue()
        {
            return SalaryRepo.getExtaravlue();
        }

        public int CalculatetotalOverTime(DateTime targetdate, int id)
        {
            return SalaryRepo.CalculatetotalOverTime(targetdate, id);
        }

        public List<SalaryWithAttend_Emp_GenSettingsVM> SalaryReport()
        {
            return SalaryRepo.SalaryReport();
        }

        public List<SalaryWithAttend_Emp_GenSettingsVM> SalaryReport(int id, DateTime targetdate)
        {
            return SalaryRepo.SalaryReport(id, targetdate);
        }

        public SalaryWithAttend_Emp_GenSettingsVM EmpolyeeSalaryReport(int id, dateformual targetdate)
        {
            return SalaryRepo.EmpolyeeSalaryReport(id, targetdate);
        }
    }
}
