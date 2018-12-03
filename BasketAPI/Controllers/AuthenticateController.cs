using BasketAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;

        public AuthenticateController(TokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost(Name = "Authenticate")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        public ActionResult Post()
        {
            return Ok(new AuthenticationResponse(_tokenGenerator.NewToken()));
        }
    }

    public class AuthenticationResponse
    {
        public AuthenticationResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}