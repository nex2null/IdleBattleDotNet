using System.Collections.Generic;
using Framework.BattleSystem;
using Framework.BattleSystem.Enums;
using Framework.BattleSystem.Gambits;
using Framework.BattleSystem.Gambits.Conditions;

namespace Framework
{
    public class PlayerCharacter
    {
        // Properties
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Str { get; set; }
        public int Int { get; set; }
        public int Spd { get; set; }

        /// <summary>
        /// Converts this player character to a battle character
        /// </summary>
        public BattleCharacter ToBattleCharacter()
        {
            return new BattleCharacter
            {
                Name = Name,
                Level = Level,
                Hp = Hp,
                Mp = Mp,
                Str = Str,
                Int = Int,
                Spd = Spd,
                CharacterType = BattleCharacterTypeEnum.PlayerParty,
                HostileToCharacterType = BattleCharacterTypeEnum.EnemyParty,
                Gambits = new List<GambitAction>
                {
                    new GambitAction(new EnemyAnyCondition(), null, GambitTypeEnum.Skill, "Attack")
                }
            };
        }
    }
}
