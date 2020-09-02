using LearnMeAPI.Data;
using LearnMeAPI.DTOs;
using LearnMeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace LearnMeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;

        public AuthController(IAuthRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            userForRegister.Username = userForRegister.Username.ToLower();

            if (await _repository.UserExist(userForRegister.Username))
                return BadRequest("Użytkownik o takiej nazwie już istnieje !");

            var userToCreate = new User
            {
                Username = userForRegister.Username,
                FirstName = userForRegister.FirstName,
                LastName = userForRegister.LastName,
                Address = userForRegister.Address,
                Postcode = userForRegister.Postcode,
                City = userForRegister.City,
                Telephone = userForRegister.Telephone,
                Email = userForRegister.Email,
                NIP = userForRegister.NIP,
                PESEL = userForRegister.PESEL
            };

            var createdUser = await _repository.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);
        }
    }
}
