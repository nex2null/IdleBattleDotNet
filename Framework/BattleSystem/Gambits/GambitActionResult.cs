using System.Collections.Generic;
using Framework.BattleSystem.Enums;
using Framework.BattleSystem.Skills;

namespace Framework.BattleSystem.Gambits
{
    public class GambitActionResult
    {
        // Properties
        public ISkill Skill { get; set; }
        public List<BattleCharacter> Targets { get; set; }
        public GambitTypeEnum Type { get; set; }
    }
}
