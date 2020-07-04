using System.Collections.Generic;
using dotnet_rpg.Model;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
         List<Character> GetAllCharacters();

         Character GetCharacterById(int pId);

         List<Character> AddCharacter(Character pCharacter);
    }
}