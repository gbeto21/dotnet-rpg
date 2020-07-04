using System.Collections.Generic;
using dotnet_rpg.Model;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{ Id= 1, Name = "Sam"}
        };
        public async Task<List<Character>> AddCharacter(Character pCharacter)
        {
            characters.Add(pCharacter);
            return characters;
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            return characters;
        }

        public async Task<Character> GetCharacterById(int pId)
        {
            return characters.FirstOrDefault(c=> c.Id == pId);
        }
    }
}