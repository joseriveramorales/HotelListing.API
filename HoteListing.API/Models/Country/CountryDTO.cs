using HoteListing.API.Models.Hotel;
using System.ComponentModel.DataAnnotations;

namespace HoteListing.API.Models.Country
{
    public class CountryDTO : BaseCountryDTO
    {
        public List<HotelDTO>? Hotels { get; set; }
    }
}
