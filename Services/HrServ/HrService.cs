using HRSystem.Repositories.HrRepo;

namespace HRSystem.Services.HrServ
{
    public class HrService : IHrService
    {
        private readonly IHrRepository HrRepo;
        public HrService(IHrRepository HrRepo)
        {
            this.HrRepo = HrRepo;
        }
    }
}
