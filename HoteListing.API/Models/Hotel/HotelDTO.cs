using System.ComponentModel.DataAnnotations.Schema;

namespace HoteListing.API.Models.Hotel
{
    public class HotelDTO
    {   
        public required string Name { get; set; }
        public required string Address { get; set; }
        public double Rating { get; set; }
    }
}
