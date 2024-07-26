using AutoMapper;
using HoteListing.API.Contracts;
using HoteListing.API.Data;
using HoteListing.API.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HoteListing.API.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper Mapper;
        private readonly UserManager<APIUser> Manager;
        private readonly IConfiguration _config;
        private APIUser _user;

        private const string _loginProvider = "HotelListingApi";
        private const string _refreshToken = "RefreshToken";

        public AuthManager(IMapper mapper, UserManager<APIUser> manager, IConfiguration config) 
        {
            Mapper = mapper;
            Manager = manager;
            _config = config;
        }


        public async Task<string> CreateRefreshToken()
        {
            await Manager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
            var newRefreshToken = await Manager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
            var result = await Manager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

            return newRefreshToken;

        }

        /// <summary>
        ///     Only return that login was valid in the case that we found user, and we were able to check the password.
        ///     Don't really want to share more information that this for now.
        /// </summary>
        public async Task<AuthResponseDto> Login(LoginDTO loginDto)
        {
            var loginIsValid = false;
            _user = await Manager.FindByEmailAsync(loginDto.Email);
            if (_user is not null)
            {
                loginIsValid = await Manager.CheckPasswordAsync(_user, loginDto.Password);
            }
            
            if (loginIsValid)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }
            return null;
        }


        /// <summary>
        ///     Here Im deciding that the Email that I receive for my users will also act as my username
        ///     Manager has probably all the functionality that I would need regarding Idendity Framework for my users
        /// </summary>
        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            _user = Mapper.Map<APIUser>(userDto);

            _user.UserName = userDto.Email;
            _user.PhoneNumber = "";
            
            var result = await Manager.CreateAsync(_user, userDto.Password);

            if (result.Succeeded)
            {
                await Manager.AddToRoleAsync(_user, "User");
            }
            
            return result.Errors;
        }

        public async Task<AuthResponseDto> VerifyRefreshToken(AuthResponseDto request)
        {
            var jwtSecutiryTokenHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecutiryTokenHandler.ReadJwtToken(request.Token);
            var username = tokenContent.Claims.ToList().FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value;
            _user = await Manager.FindByNameAsync(username);
            if (_user == null || _user.Id != request.UserId)
            {
                return null;
            }
            var isValidRefreshToken = await Manager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.RefreshToken);

            if (isValidRefreshToken)
            {
                var token = await GenerateToken();
                return new AuthResponseDto
                {
                    Token = token,
                    UserId = _user.Id,
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await Manager.UpdateSecurityStampAsync(_user);
            return null;
        }

        private async Task<string> GenerateToken()
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await Manager.GetRolesAsync(_user);
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var userClaims = await Manager.GetClaimsAsync(_user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user.Email), // Sub claim means the person to whom this token belongs
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", _user.Id),

            }
            .Union(userClaims).Union(roleClaims);
            
            // here I need to populate my Jwt token with what I defined, refer to the initializaion of my token in program.cs
            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
