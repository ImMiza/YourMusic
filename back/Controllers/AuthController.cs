using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using yourmusic.Models;
using yourmusic.Context;
using Microsoft.AspNetCore.Authorization;
using yourmusic.Auth;

namespace yourmusic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    class AuthController : ControllerBase
    {
        private readonly ILogger<Music> _logger;
        private ContextDatabase _db;

        private AuthManager _auth;

        public AuthController(ILogger<Music> logger, ContextDatabase db, AuthManager auth)
        {
            this._logger = logger;
            this._db = db;
            this._auth = auth;
        }

        [HttpGet]
        public IActionResult GetActionResult() {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PostConnection(string user, string password) {
            var token = _auth.Connection(user, password);
            if(token == null) {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}