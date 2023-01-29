using AutoMapper;
using FriendsAuth.Models;
using FriendsCore;
using FriendsData;
using FriendsData.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FriendsAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        private IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthController( UnitOfWork unitOfWork,
            IConfiguration config, 
            IMapper mapper )
        {
            this._unitOfWork = unitOfWork;
            this._config = config;
            this._mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login( LoginModel model )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = this._unitOfWork.UsersRepository.GetWithIncludeAll()
                .FirstOrDefault(_ => _.Email.ToLower() == model.Email.ToLower()
               && _.Password == model.Password);

            if(user is null)
            {
                return NotFound("User not found");
            }


            var token = GetToken(user);

            return Ok(token);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<RegisterModel, User>(model);
            user.Role = GetDefaultRole();
            user.RefreshToken = new RefreshToken()
            {
                Value = new Guid()
            };

            await _unitOfWork.UsersRepository.AddAsync(user);

            var token = GetToken(user);

            return Ok(token);
        }

        private string GetToken( User user )
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtBearer:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, user.Role.RoleType.ToString())
            };

            var permissions = user.Role.RolePermissions
                .Select(_ => new Claim("permission", _.PermissionType.ToString()));

            claims.AddRange(permissions);

            var tokenLifeTime = int.Parse(_config["TokenLifeTime"]);

            var jwt = new JwtSecurityToken(
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(tokenLifeTime),
                claims: claims,
                signingCredentials: credentials);

            var refreshToken = new Guid();

            if(user.RefreshToken is null)
            {
                user.RefreshToken = new RefreshToken() { Value = refreshToken };
            }
            else 
            {
                user.RefreshToken.Value = refreshToken;
            }

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }

        public Role GetDefaultRole()
        {
            var role = new Role();

            role.RoleType = RoleType.User;

            role.RolePermissions = new List<RolePermission>();

            role.RolePermissions.Add(
                new RolePermission() 
                { 
                    PermissionType = PermissionType.ReadEvent 
                });

            role.RolePermissions.Add(
                new RolePermission()
                {
                    PermissionType = PermissionType.WriteEvent
                });

            role.RolePermissions.Add(
                new RolePermission()
                {
                    PermissionType = PermissionType.DeleteEvent
                });

            role.RolePermissions.Add(
                new RolePermission()
                {
                    PermissionType = PermissionType.JoinEvent
                });

            return role;
        }
    }
}
