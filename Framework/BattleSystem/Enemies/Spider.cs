using System.Collections.Generic;
using Framework.BattleSystem.Enums;
using Framework.BattleSystem.Gambits.Conditions;
using Framework.Itemization;
using Framework.Itemization.Enums;

namespace Framework.BattleSystem.Enemies
{
    public class Spider : BattleCharacter
    {
        public Spider(string name)
        {
            // Initialize base stats
            Name = name;
            Level = 1;
            Hp = 300;
            Mp = 0;
            Str = 2;
            Spd = 7;
            CharacterType = BattleCharacterTypeEnum.EnemyParty;
            HostileToCharacterType = BattleCharacterTypeEnum.PlayerParty;

            // Initialize gambits
            Gambits = new List<Gambits.GambitAction>
            {
                new Gambits.GambitAction(new SelfCondition(), null, GambitTypeEnum.Skill, "Defend", 10),
                new Gambits.GambitAction(new EnemyAnyCondition(), null, GambitTypeEnum.Skill, "Web Shoot", 10),
                new Gambits.GambitAction(new EnemyAnyCondition(), null, GambitTypeEnum.Skill, "Attack")
            };

            // Initialize drops
            MaxNumberOfItemsToDrop = 1;
            LootGenerationOptions = new List<LootGenerationOption>
            {
                new LootGenerationOption(null, ItemSuperTypeEnum.Equipment, null, 1, 25),
                new LootGenerationOption(null, ItemSuperTypeEnum.Currency, null, 1, 25)
            };
        }
    }
}
