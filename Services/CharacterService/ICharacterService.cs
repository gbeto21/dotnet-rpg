using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Model;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
         Task<List<Character>> GetAllCharacters();

         Task<Character> GetCharacterById(int pId);

         Task<List<Character>> AddCharacter(Character pCharacter);
    }
}