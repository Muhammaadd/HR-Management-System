namespace HRSystem.Repositories.GeneralSettingRepo
{
    public interface IGeneralSettingRepository
    {
        List<GeneralSetting> GetAll();
        GeneralSetting GetById(int id);
        void Insert(GeneralSetting NewGeneralSetting);
        int OverTimePricePerHour();
        int DeductionPricePerHour();
    }
}
