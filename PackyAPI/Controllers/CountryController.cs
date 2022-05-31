using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{id:int}")]
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
    }
}
