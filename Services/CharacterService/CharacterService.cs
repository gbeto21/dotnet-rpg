using System.Collections.Generic;
using dotnet_rpg.Model;
using System.Linq;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character{ Id= 1, Name = "Sam"}
        };
        public List<Character> AddCharacter(Character pCharacter)
        {
            characters.Add(pCharacter);
            return characters;
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        public Character GetCharacterById(int pId)
        {
            return characters.FirstOrDefault(c=> c.Id == pId);
        }
    }
}