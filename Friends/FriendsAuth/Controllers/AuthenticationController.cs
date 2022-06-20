using FriendsAuth.Models;
using FriendsData;
using FriendsData.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FriendsAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UnitOfWork _unitOfWork;

        public AuthenticationController( UnitOfWork unitOfWork, Mapper )
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login( LoginModel model )
        {
            var exists = _unitOfWork.UsersRepository.Get().Any(usr => usr.Email.Equals(model.Email));

            if(!exists)
            {
                return Unauthorized();
            }

            return Ok();
        }

        [HttpPost] IActionResult Register(RegisterModel model)
        {

            var existsWithEmail = _unitOfWork.UsersRepository.Get().Any(usr => usr.Email.Equals(model.Email));

            if(existsWithEmail)
            {
                return Conflict();
            }

            var existsWithPhone = _unitOfWork.UsersRepository.Get().Any(usr => usr.PhoneNumber.Equals(model.PhoneNumber));

            if (existsWithPhone)
            {
                return Conflict();
            }
            var newUser = new User();
            
            this._unitOfWork.UsersRepository.AddAsync(newUser);

            return Ok();
        }


    }
}
