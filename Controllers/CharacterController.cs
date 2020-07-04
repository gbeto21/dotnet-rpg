using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Model;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService pCharacterService)
        {
            this.characterService = pCharacterService;
        }

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
        public async Task<IActionResult> AddCaracter(Character pCharacter)
        {
            return Ok(await characterService.AddCharacter(pCharacter));
        }

    }
}