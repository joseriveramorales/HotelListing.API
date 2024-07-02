using AutoMapper;
using HoteListing.API.Contracts;
using HoteListing.API.Data;
using HoteListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace HoteListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper Mapper;
        private readonly UserManager<APIUser> Manager;

        public AuthManager(IMapper mapper, UserManager<APIUser> manager) 
        {
            Mapper = mapper;
            Manager = manager;
        }

        /// <summary>
        ///     Here Im deciding that the Email that I receive for my users will also act as my username
        ///     Manager has probably all the functionality that I would need regarding Idendity Framework for my users
        /// </summary>
        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            var user = Mapper.Map<APIUser>(userDto);

            user.UserName = userDto.Email;
            user.PhoneNumber = "";
            
            var result = await Manager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                await Manager.AddToRoleAsync(user, "User");
            }
            
            return result.Errors;
        }
    }
}
