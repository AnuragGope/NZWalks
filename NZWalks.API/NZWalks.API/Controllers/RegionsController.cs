using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository=regionRepository;
        }

        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = regionRepository.getData();
            return Ok(regions);
        }
    }
}
