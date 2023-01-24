using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> getAllAsync();
        Task<Walk> GetAsync(Guid id);
        Task<Walk> addAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> deleteAsync(Guid id);
    }
}
