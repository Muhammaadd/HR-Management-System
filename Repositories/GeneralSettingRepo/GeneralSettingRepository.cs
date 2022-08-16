using HRSystem.Models;

namespace HRSystem.Repositories.GeneralSettingRepo
{
    public class GeneralSettingRepository : IGeneralSettingRepository
    {
        private readonly HRDbContext context;
        public GeneralSettingRepository(HRDbContext context)
        {
            this.context = context;
        }
        public int OverTimePricePerHour()
        {
            return context.GeneralSettings.Select(n => n.ValueOfExtra).FirstOrDefault();
        }
        public int DeductionPricePerHour()
        {
            return context.GeneralSettings.Select(n => n.ValueOfDiscount).FirstOrDefault();
        }
        public List<GeneralSetting> GetAll()
        {
            return context.GeneralSettings.ToList();
        }
        public GeneralSetting GetById(int id)
        {
            return context.GeneralSettings.FirstOrDefault(n => n.Id == id);
        }
        public void Insert(GeneralSetting NewGeneralSetting)
        {
            GeneralSetting DeafultSetting = GetById(1);
            if (DeafultSetting == null)
            {
                NewGeneralSetting.Id = 1;
                context.GeneralSettings.Add(NewGeneralSetting);
            }
            else
            {
                DeafultSetting.ValueOfDiscount = NewGeneralSetting.ValueOfDiscount;
                DeafultSetting.ValueOfExtra = NewGeneralSetting.ValueOfExtra;
                context.GeneralSettings.Update(DeafultSetting);
            }
            context.SaveChanges();
        }
    }
}
