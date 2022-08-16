using HRSystem.Models;

namespace HRSystem.Repositories.HrRepo
{
    public class HrRepository : IHrRepository
    {
        private readonly HRDbContext context;
        public HrRepository(HRDbContext context)
        {
            this.context = context;
        }
    }
}
