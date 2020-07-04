using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Model;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService pCharacterService)
        {
            this.characterService = pCharacterService;
        }

        // [AllowAnonymous]
        // [Route("getall")]
        [HttpGet("getall")]
        public async Task<IActionResult> Get()
        {
            return Ok(await characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCaracter(AddCharacterDto pCharacter)
        {
            return Ok(await characterService.AddCharacter(pCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCaracter(UpdateCharacterDto pCharacter)
        {
            ServiceResponse<GetCharacterDto> response = await characterService.UpdateCharacter(pCharacter);
            if(response.Data == null)
                return NotFound(response);
            
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = await characterService.DeleteCharacter(id);
            if(response.Data == null)
                return NotFound(response);
            
            return Ok(response);
        }

    }
}