using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository iregionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository iregionRepository, IMapper mapper)
        {
            this.iregionRepository=iregionRepository;
            this.mapper=mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await iregionRepository.getDataAsync();
            // return DTO regions
            /*            var regionsDTO = new List<Models.DTO.Region>();
                        regions.ToList().ForEach(region =>
                        {
                            var regionDTO = new Models.DTO.Region()
                            {
                                id = region.id,
                                Code = region.Code,
                                Name = region.Name,
                                Area = region.Area,
                                Lat = region.Lat,
                                Long = region.Long,
                                Population = region.Population
                            };

                            regionsDTO.Add(regionDTO);
                        });*/
            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await iregionRepository.getAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            // Validate the request
/*            if (!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }*/

            // Request(DTO) to Domain Model
            var region = new Models.Domain.Region()
            {
                Code=addRegionRequest.Code,
                Area=addRegionRequest.Area,
                Lat=addRegionRequest.Lat,
                Long=addRegionRequest.Long,
                Name=addRegionRequest.Name,
                Population=addRegionRequest.Population
            };

            // Pass the details to Repo
            region = await iregionRepository.addAsync(region);

            // Convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                id=region.id,
                Code=region.Code,
                Area=region.Area,
                Lat=region.Lat,
                Long=region.Long,
                Name=region.Name,
                Population=region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.id }, regionDTO);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // get region from database
            var region = await iregionRepository.deleteAsync(id);

            // If Null NotFound
            if (region == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var regionDTO = new Models.DTO.Region()
            {
                id=region.id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            // return Ok response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // Convert DTO to Domain model
            var region = new Models.Domain.Region()
            {
                Name = updateRegionRequest.Name,
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };
            // Update region using repository
            region = await iregionRepository.updateAsync(id, region);

            // If Null then not found
            if (region == null)
            {
                return NotFound();
            }
            // Convert domain back to Dto
            var regionDTO = new Models.DTO.Region()
            {
                id=region.id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            // Return Ok response
            return Ok(regionDTO);
        }

        #region Private Methods
        private bool ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                    $"{nameof(addRegionRequest.Code)} cannot be null or empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name),
                    $"{nameof(addRegionRequest.Name)} cannot be null or empty or whitespace");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}