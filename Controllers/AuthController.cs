using System.Threading.Tasks;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.User;
using dotnet_rpg.Model;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(IAuthRepository pAuthRepository)
        {
            this.authRepository = pAuthRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request){
            ServiceResponse<int> response = await authRepository.Register(
                new User { UserName = request.UserName} ,request.Password 
            );
            if(response.Success == false)
                return BadRequest(response);
            
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request){
            
            ServiceResponse<string> response = await authRepository.Login(
                request.UserName ,request.Password 
            );
            if(response.Success == false)
                return BadRequest(response);
            
            return Ok(response);
        }
        
    }
}