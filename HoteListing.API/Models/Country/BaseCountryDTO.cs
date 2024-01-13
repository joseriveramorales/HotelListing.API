using System.ComponentModel.DataAnnotations;

namespace HoteListing.API.Models.Country
{
    public abstract class BaseCountryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ShortName { get; set; }
    }
}
