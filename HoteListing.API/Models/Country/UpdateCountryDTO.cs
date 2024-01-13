using System.ComponentModel.DataAnnotations;

namespace HoteListing.API.Models.Country
{
    public class UpdateCountryDTO : BaseCountryDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
