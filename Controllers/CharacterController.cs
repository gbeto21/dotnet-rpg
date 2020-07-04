using System.Collections.Generic;
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
        public IActionResult Get()
        {
            return Ok(characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {
            return Ok(characterService.GetCharacterById(id));
        }

        [HttpPost]
        public IActionResult AddCaracter(Character pCharacter)
        {
            return Ok(characterService.AddCharacter(pCharacter));
        }

    }
}