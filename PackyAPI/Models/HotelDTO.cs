using System.ComponentModel.DataAnnotations;

namespace PackyAPI.Models
{
    public class CreateHotelDTO
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "Hotel name is too long/short")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 10, ErrorMessage = "Address is too long/short")]
        public string Address { get; set; }
        [Required]
        [Range(0, 5)]
        public double Rating { get; set; }
        [Required]
        public int CountryId { get; set; }

    }
    public class HotelDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }

    }
}
