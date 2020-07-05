using System.Collections.Generic;
using dotnet_rpg.Model;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using AutoMapper;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly IMapper mapper;
        public CharacterService(IMapper pMapper, DataContext pContext, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.context = pContext;
            this.mapper = pMapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto pCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = mapper.Map<Character>(pCharacter);
            character.User = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            await context.Characters.AddAsync(character);
            await context.SaveChangesAsync();
            serviceResponse.Data = (context.Characters.Where(c =>c.User.Id == GetUserId()).Select(c => mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int pId)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character character = await context.Characters
                    .FirstOrDefaultAsync(c => c.Id == pId && c.User.Id == GetUserId());
                if(character !=null){
                    context.Characters.Remove(character);
                    await context.SaveChangesAsync();
                    serviceResponse.Data = (context.Characters.Where(c => c.User.Id == GetUserId())
                        .Select(c => mapper.Map<GetCharacterDto>(c))).ToList();   
                }
                else
                {
                    serviceResponse.Success = false; 
                    serviceResponse.Message = "Character not found.";
                }

            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = await context.Characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = (dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int pId)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await context.Characters
            .Include(c=> c.Weapon)
            .Include(c => c.CharacterSkills).ThenInclude(cs=>cs.Skill)
            .FirstOrDefaultAsync(c => c.Id == pId && c.User.Id == GetUserId());
            serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await context.Characters.Include(c=> c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if(character.User.Id == GetUserId())
                {
                    character.Name = updatedCharacter.Name;
                    character.Class = updatedCharacter.Class;
                    character.Defense = updatedCharacter.Defense;
                    character.HitPoints = updatedCharacter.Intelligence;
                    character.Strength = updatedCharacter.Strength;
                    character.Intelligence = updatedCharacter.Intelligence;

                    context.Characters.Update(character);
                    await context.SaveChangesAsync();

                    serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
                }
                else{
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }
            }
            catch (System.Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;

        }
    
        private int GetUserId() => int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    
    }
}