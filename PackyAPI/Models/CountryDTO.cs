using System.ComponentModel.DataAnnotations;

namespace PackyAPI.Models
{
    public class CreateCountryDTO
    {

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Country name too long")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 3, ErrorMessage = "Short form too long")]
        public string shortname { get; set; }
    }
    public class UpdateCountryDTO : CreateCountryDTO
    {
        public virtual IList<CreateHotelDTO> Hotels { get; set; }
    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }

        public virtual IList<HotelDTO> Hotels { get; set; }

    }

}
