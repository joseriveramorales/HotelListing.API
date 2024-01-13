using System.ComponentModel.DataAnnotations;

namespace HoteListing.API.Models.Country
{
    /// <summary>
    ///     DTO for creating a country. 
    ///     My DTOs give me the capability to avoid exposing my actual Data Models into the API. 
    ///     It is a way of me defining constraints over what is possible with my Data Models.
    /// </summary>
    public class CreateCountryDTO : BaseCountryDTO
    {

    }
}
