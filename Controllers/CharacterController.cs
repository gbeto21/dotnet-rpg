using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Model;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController: ControllerBase
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{ Id= 1, Name = "Sam"}
        };
        
        // [Route("getall")]
        [HttpGet("getall")]
        public IActionResult Get(){
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id){
            return Ok(characters.FirstOrDefault(c=> c.Id == id));
        }
    }
}