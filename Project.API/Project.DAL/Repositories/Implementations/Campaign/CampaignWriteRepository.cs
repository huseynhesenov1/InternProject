using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Campaign;

namespace Project.DAL.Repositories.Implementations.Campaign
{
    public class CampaignWriteRepository : WriteRepository<Core.Entities.Campaign>, ICampaignWriteRepository
    {
        public CampaignWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
    
}
