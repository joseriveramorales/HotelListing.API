using System.ComponentModel.DataAnnotations;

namespace HoteListing.API.Models.Country
{
    public abstract class BaseCountryDTO
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string ShortName { get; set; }
    }
}
