using System.ComponentModel.DataAnnotations;

namespace PackyAPI.Data
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string shortname { get; set; }
        public virtual IList<Hotel> Hotels { get; set; }
    }
}
