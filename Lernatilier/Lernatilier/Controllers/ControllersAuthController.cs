using Microsoft.AspNetCore.Mvc;
using FitnessTrackerAPI.Models;
using FitnessTrackerAPI.Helpers;

namespace FitnessTrackerAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string FilePath = "users.json";

        [HttpPost("register")]
        public ActionResult<Benutzer> Register(Benutzer user)
        {
            var users = JsonFileHelper.Load<Benutzer>(FilePath);

            if (users.Any(u => u.Benutzername == user.Benutzername))
                return BadRequest("Benutzername existiert bereits.");

            user.Id = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);

            JsonFileHelper.Save(FilePath, users);
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<Benutzer> Login(Benutzer loginUser)
        {
            var users = JsonFileHelper.Load<Benutzer>(FilePath);

            var user = users.FirstOrDefault(u => u.Benutzername == loginUser.Benutzername && u.Passwort == loginUser.Passwort);

            if (user == null)
                return Unauthorized("Benutzername oder Passwort ist falsch.");

            return Ok(user);
        }
    }
}
