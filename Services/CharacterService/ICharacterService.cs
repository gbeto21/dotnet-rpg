using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Model;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
         Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();

         Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int pId);

         Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto pCharacter);

         Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);

         Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int pId);
    }
}