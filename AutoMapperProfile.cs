using System.Linq;
using AutoMapper;
using dotnet_rpg.Dtos;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Model;

namespace dotnet_rpg
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>()
                .ForMember(dto => dto.Skills, c => c.MapFrom(c=> c.CharacterSkills.Select(cs=>cs.Skill)));
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}