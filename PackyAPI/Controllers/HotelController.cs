using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackyAPI.IRepository;
using PackyAPI.Models;

namespace PackyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IUnitofWork _unitofwork;
        private readonly ILogger<CountryController> _logger;
        private readonly IMapper _mapper;

        public HotelController(IUnitofWork unitofwork, ILogger<CountryController> logger, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            try
            {
                //throw new NotImplementedException();
                var hotels = await _unitofwork.Hotels.GetAll();
                var results = _mapper.Map<IList<HotelDTO>>(hotels);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while calling {nameof(GetHotels)}");
                return StatusCode(500, "Internal Server Error Pwease try later");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHotel(int id)
        {
            try
            {
                //throw new NotImplementedException();
                var hotel = await _unitofwork.Hotels.Get(o => o.Id == id, new List<string> { "Country" });
                var result = _mapper.Map<HotelDTO>(hotel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while calling {nameof(GetHotel)}");
                return StatusCode(500, "Internal Server Error Pwease try later");
            }
        }
    }
}
