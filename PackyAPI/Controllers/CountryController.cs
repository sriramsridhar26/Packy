using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PackyAPI.Data;
using PackyAPI.IRepository;
using PackyAPI.Models;

namespace PackyAPI.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IUnitofWork _unitofwork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public CountryController(IUnitofWork unitofwork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                //throw new NotImplementedException();
                var countries = await _unitofwork.Countries.GetAll();
                var results = _mapper.Map<IList<CountryDTO>>(countries);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while calling {nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Error Pwease try later");
            }
        }
        [HttpGet("{id:int}", Name ="GetCountry")]
        public async Task<IActionResult> GetCountry(int id)
        {
            try
            {
                //throw new NotImplementedException();
                var country = await _unitofwork.Countries.Get(o => o.Id==id, new List<string> { "Hotels"});
                var result = _mapper.Map<CountryDTO>(country);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while calling {nameof(GetCountries)}");
                return StatusCode(500, "Internal Server Error Pwease try later");
                //return StatusCode(500, "Internal Server Error Pwease try later");
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCountry([FromBody] CreateCountryDTO country)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Create Hotel failed");
                return BadRequest(ModelState);
            }
            try
            {
                var countr = _mapper.Map<Country>(country);
                await _unitofwork.Countries.Insert(countr);
                await _unitofwork.Save();
                return CreatedAtRoute("GetHotel", new { id = countr.Id }, countr);
            }
            catch (Exception ex)
            {
                return BadRequest("Internal server error");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] UpdateCountryDTO country)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogInformation($"Update Hotel failed");
                return BadRequest(ModelState);
            }
            try
            {
                var countr = await _unitofwork.Countries.Get(q => q.Id == id);
                if (countr == null)
                {
                    return BadRequest("submitted req does not exist");
                }
                else
                {
                    _mapper.Map(country, countr);
                    _unitofwork.Countries.Update(countr);
                    _unitofwork.Save();
                    return CreatedAtRoute("GetCountry", new { id = countr.Id }, countr);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Internal server error");
            }
        }


    }
}
