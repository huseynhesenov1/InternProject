using Project.DAL.Contexts;
using Project.DAL.Repositories.Abstractions.Campaign;

namespace Project.DAL.Repositories.Implementations.Campaign
{
    public class CampaignReadRepository : ReadRepository<Core.Entities.Campaign>, ICampaignReadRepository
    {
        public CampaignReadRepository(AppDbContext context) : base(context)
        {
        }
    }
     
}
