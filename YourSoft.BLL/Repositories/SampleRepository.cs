using YourSoft.BLL.Contracts;
using YourSoft.DAL.Data;

namespace YourSoft.BLL.Repositories
{
    public class SampleRepository : GenericRepository<Sample>, ISampleRepository
    {
        public SampleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
