using HRSystem.Repositories.GeneralSettingRepo;

namespace HRSystem.Services.GeneralSettingServ
{
    public interface IGeneralSettingService : IGeneralSettingRepository
    {
        GeneralSettingViewModel GetGeneralSettingViewModel();
        void DeleteAll();
        GeneralSetting GetById(int id);
        void Insert(GeneralSetting NewGeneralSetting);
    }
}
