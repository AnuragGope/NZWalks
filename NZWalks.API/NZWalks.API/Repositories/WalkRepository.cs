using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext=nZWalksDbContext;
        }

        public async Task<Walk> addAsync(Walk walk)
        {
            walk.id = Guid.NewGuid();
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk> deleteAsync(Guid id)
        {
            var existingwalk = await nZWalksDbContext.Walks.FindAsync(id);
            if (existingwalk==null)
            {
                return null;
            }
            nZWalksDbContext.Walks.Remove(existingwalk);
            await nZWalksDbContext.SaveChangesAsync();

            return existingwalk;
        }

        public async Task<IEnumerable<Walk>> getAllAsync()
        {
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingwalk = await nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.id == id);
            if (existingwalk == null)
            {
                return null;
            }
            existingwalk.id=walk.id;
            existingwalk.Name=walk.Name;
            existingwalk.Length=walk.Length;
            existingwalk.WalkDifficultyId=walk.WalkDifficultyId;
            existingwalk.RegionId=walk.RegionId;

            await nZWalksDbContext.SaveChangesAsync();

            return existingwalk;

        }
    }
}
