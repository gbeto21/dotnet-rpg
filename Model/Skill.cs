using System.Collections.Generic;

namespace dotnet_rpg.Model
{
    public class Skill
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }

        public List<CharacterSkill> CharacterSkills { get; set; }
    }
}