using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext=nZWalksDbContext;
        }

        public IEnumerable<Region> getData()
        {
            return nZWalksDbContext.Regions.ToList();
        }
    }
}
