 using HotelListing_API.Contracts;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing_API.Features.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public ValuesController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody]ApiUserModel entity)
        {
            var errors = await _authManager.Register(entity);
            if (errors.Any())
            {
                foreach (var e in errors)
                {
                    ModelState.AddModelError(e.Code,e.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();    
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody]LoginModel entity)
        {
          var authResponse =await _authManager.Login(entity);
          if (authResponse is null)
              return Unauthorized();
          return Ok(authResponse);
        }

    }
}
