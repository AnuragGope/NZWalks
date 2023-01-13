using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> getDataAsync();
        Task<Region> getAsync(Guid id);
        Task<Region> addAsync(Region region);
        Task<Region> deleteAsync(Guid id);
        Task<Region> updateAsync(Guid id, Region region);
    }
}
