using LegoSets.Dtos.LegoSets;
using LegoSets.Services.LegoSetService;
using Microsoft.AspNetCore.Mvc;

namespace LegoSets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LegoSetController : ControllerBase
    {
        public ILegoSetService _legoSetService { get; }
        public LegoSetController(ILegoSetService legoSetService)
        {
            _legoSetService = legoSetService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetLegoSetDto>>>> GetAll()
        {
            return Ok(await _legoSetService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetLegoSetDto>>> GetLegoSetById(int id)
        {
            return Ok(await _legoSetService.GetLegoSetByID(id));
        }

        [HttpGet("readCsvFile")]
        public async Task<ActionResult<ServiceResponse<List<GetLegoSetDto>>>> readCsvLegoSets()
        {
            return Ok(await _legoSetService.readCsvLegoSets());
        }

        [HttpGet("sorting")]
        public async Task<ActionResult<ServiceResponse<IOrderedEnumerable<GetLegoSetDto>>>> getLegosetBySorting(string? sortOrder, string? searchString)
        {
            return Ok(await _legoSetService.SortingAndFilter(sortOrder, searchString));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetLegoSetDto>>>> Add(AddLegoSetDto legoSetDto)
        {
            return Ok(await _legoSetService.Add(legoSetDto));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetLegoSetDto>>> Update(int id, UpdateLegoSetDto updateLegoSet)
        {
            var serviceResponse = await _legoSetService.Update(id, updateLegoSet);
            if(serviceResponse == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpPut("{id}/Owned")]
        public async Task<ActionResult<ServiceResponse<GetLegoSetDto>>> UpdateLegoSet(int id, bool Owned)
        {
            var serviceResponse = await _legoSetService.UpdateOwned(id, Owned);
            if (serviceResponse == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetLegoSetDto>>> Delete(int id)
        {
            var serviceResponse = await _legoSetService.Delete(id);
            if(serviceResponse == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
        
    }
}
