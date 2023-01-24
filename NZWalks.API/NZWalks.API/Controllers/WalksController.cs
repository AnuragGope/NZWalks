using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository iwalkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository iwalkRepository, IMapper mapper)
        {
            this.iwalkRepository=iwalkRepository;
            this.mapper=mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walk = await iwalkRepository.getAllAsync();
            var walkDTO = mapper.Map<List<Models.DTO.Walk>>(walk);
            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            var walk = await iwalkRepository.GetAsync(id);
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            var walk = new Models.Domain.Walk()
            {
                Name=addWalkRequest.Name,
                Length=addWalkRequest.Length,
                RegionId=addWalkRequest.RegionId,
                WalkDifficultyId=addWalkRequest.WalkDifficultyId
            };

            walk = await iwalkRepository.addAsync(walk);

            var walkDTO = new Models.DTO.Walk()
            {
                id=walk.id,
                Name=walk.Name,
                Length=walk.Length,
                RegionId=walk.RegionId,
                WalkDifficultyId=walk.WalkDifficultyId
            };

            // return Ok(walkDTO);
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public  async Task<IActionResult> UpdateWalkAsync(Guid id,Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            var walkData = new Models.Domain.Walk
            {
                Name=updateWalkRequest.Name,
                Length=updateWalkRequest.Length,
                RegionId=updateWalkRequest.RegionId,
                WalkDifficultyId=updateWalkRequest.WalkDifficultyId
            };

            walkData = await iwalkRepository.UpdateAsync(id, walkData);

            if (walkData==null)
            {
                return NotFound();
            }

            var walkDTO = new Models.DTO.Walk
            {
                id=walkData.id,
                Name=walkData.Name,
                Length=walkData.Length,
                RegionId=walkData.RegionId,
                WalkDifficultyId=walkData.WalkDifficultyId
            };

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        // removing walk from database
        public async Task<IActionResult> DelWalkAsync(Guid id)
        {
            var walk = await iwalkRepository.deleteAsync(id);
            if (walk==null)
            {
                return NotFound();
            }
            return Ok(walk);
        }
    }
}
