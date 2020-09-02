using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnMeAPI.Data;
using LearnMeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Register(string username, string firstName, string lastName,
                                                  string address, int postcode, string city, int telephone,
                                                  string email, int nip, int pesel, string password)
        {
            username = username.ToLower();

            if (await _repository.UserExist(username))
                return BadRequest("Użytkownik o takiej nazwie już istnieje !");

            var newUser = new User
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Postcode = postcode,
                City = city,
                Telephone = telephone,
                Email = email,
                NIP = nip,
                PESEL = pesel
            };

            var createUser = await _repository.Register(newUser, password);

            return StatusCode(201);
        }
    }
}
