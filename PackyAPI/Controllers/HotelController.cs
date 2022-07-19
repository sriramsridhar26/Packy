using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackyAPI.Data;
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
        [HttpGet("{id:int}", Name = "GetHotel")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDTO hotel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"Create Hotel failed");
                return BadRequest(ModelState);
            }
            try
            {
                var hote = _mapper.Map<Hotel>(hotel);
                await _unitofwork.Hotels.Insert(hote);
                await _unitofwork.Save();
                return CreatedAtRoute("GetHotel", new { id = hote.Id }, hote);
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
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] UpdateHotelDTO hotel)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogInformation($"Update Hotel failed");
                return BadRequest(ModelState);
            }
            try
            {
                var hote = await _unitofwork.Hotels.Get(q => q.Id == id);
                if (hote == null)
                {
                    return BadRequest("submitted req does not exist");
                }
                else
                {
                    _mapper.Map(hotel, hote);
                    _unitofwork.Hotels.Update(hote);
                    _unitofwork.Save();
                    return CreatedAtRoute("GetHotel", new { id = hote.Id }, hote);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Internal server error");
            }
        }
    }
}
