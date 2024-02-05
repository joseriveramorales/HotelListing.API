namespace HoteListing.API.Models.Hotel
{
    public class CreateHotelDTO : BaseHotelDTO
    {
        public required int CountryID { get; set; }
    }
}
