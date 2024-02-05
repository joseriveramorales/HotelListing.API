using AutoMapper;
using HoteListing.API.Data;
using HoteListing.API.Models.Country;
using HoteListing.API.Models.Hotel;

namespace HoteListing.API.Configurations
{
    /// <summary>
    ///     This is my class to map one class to another. Mainly, to map my DTO's onto my actual data model classes.
    ///     One main benefit of this is that my controller class will be cleaner, 
    ///     since I wont have to be consistently maintaining code to convert DTO's into data model classes.
    ///     
    /// </summary>
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            // Create Map specifices that I can create a Country using a CreateCountryDTO
            // ReverseMap specifies that I can use the reverse order, meaning create a CreateCountryDTO from a Country.
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, GetCountryDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Hotel, GetHotelDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDTO>().ReverseMap();
        }
    }
}
