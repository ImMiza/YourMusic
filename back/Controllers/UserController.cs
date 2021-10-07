using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yourmusic.Models;
using yourmusic.Context;
using Microsoft.AspNetCore.Authorization;
using yourmusic.Auth;

namespace yourmusic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<Music> _logger;
        private ContextDatabase _db;
        private AuthManager _auth;

        public UserController(ILogger<Music> logger, ContextDatabase db, AuthManager auth)
        {
            this._logger = logger;
            this._db = db;
            this._auth = auth;
        }


        [HttpGet]
        [Route("{id}")]
        public User GetUserById(int id)
        {
            return _db.Users.Find(id);
        }

        [HttpPost]
        public IActionResult PostUser(User user) {
            user.Id = 0;
            _db.Users.Add(user);
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(int id) {
            User user = _db.Users.Find(id);
            if(user == null) {
                return NoContent();
            }

            _db.Users.Remove(user);
            _db.SaveChanges();
            return Ok($"{id} is removed");
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult PutUser(int id, User user)
        {
            if(_db.Users.Find(id) == null) {
                return NoContent();
            }
            user.Id = id;

            _db.Users.Update(user);
            _db.SaveChanges();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{username}")]
        public IActionResult UserConnection(string username, string password)
        {
            var token = _auth.Connection(username, password);
            if(token == null) {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}