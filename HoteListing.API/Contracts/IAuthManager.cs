using HoteListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HoteListing.API.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
    }
}
