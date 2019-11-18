using System.Collections.Generic;
using System.Linq;
using Framework.BattleSystem.BattleEffects;
using Framework.BattleSystem.Enums;
using Framework.BattleSystem.Gambits;
using Framework.Itemization;

namespace Framework.BattleSystem
{
    public class BattleCharacter
    {
        // Properties
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Str { get; set; }
        public int Int { get; set; }
        public int Spd { get; set; }
        public int CurrentCharge { get; set; }
        public BattleCharacterTypeEnum CharacterType { get; set; }
        public BattleCharacterTypeEnum HostileToCharacterType { get; set; }
        public List<IEffect> Effects { get; set; }
        public List<GambitAction> Gambits { get; set; }

        // TODO: Refactor enemy specific things to enemy base class
        public int MaxNumberOfItemsToDrop { get; set; }
        public List<LootGenerationOption> LootGenerationOptions { get; set; }

        public BattleCharacter()
        {
            Effects = new List<IEffect>();
            Gambits = new List<GambitAction>();
            LootGenerationOptions = new List<LootGenerationOption>();
        }

        /// <summary>
        /// Updates the charge level of the character
        /// </summary>
        public void UpdateCharge()
        {
            if (!IsAlive())
                return;

            CurrentCharge += Spd;
            CurrentCharge = CurrentCharge > 100 ? 100 : CurrentCharge;
        }

        /// <summary>
        /// Determine if the character is ready to act
        /// </summary>\
        public bool IsReadyToAct()
        {
            return IsAlive() && CurrentCharge >= 100;
        }

        /// <summary>
        /// Determine if the character is still alive
        /// </summary>
        public bool IsAlive()
        {
            return Hp > 0;
        }

        /// <summary>
        /// Has the character perform an action
        /// </summary>
        public void Act(List<BattleCharacter> characters, BattleLog battleLog)
        {
            // Do any pre-action work
            BeforeActionPerformed();

            // If I am no longer ready to act after pre-action work then do nothing
            if (!IsReadyToAct())
            {
                ActionPerformed();
                return;
            }

            // Grab the first valid gambit action
            GambitActionResult gambitAction = null;
            for (var i = 0; i < Gambits.Count; i++)
            {
                gambitAction = Gambits[i].GetAction(this, characters);
                if (gambitAction != null)
                    break;
            }

            // If there is no gambit action then the user has nothing to do
            if (gambitAction == null)
            {
                battleLog.AddMessage($"{Name} wanders around aimlessly");
                ActionPerformed();
                return;
            }

            // If this is a skill action then use the skill
            if (gambitAction.Type == GambitTypeEnum.Skill)
            {
                gambitAction.Skill.Use(this, gambitAction.Targets, battleLog);
            }

            // Set that an action was performed
            ActionPerformed();
        }

        /// <summary>
        /// Applies battle damage
        /// </summary>
        public void ApplyDamage(BattleDamage damage)
        {
            // Allow effects to modify damage before processing
            Effects.ForEach(x => x.BeforeDamageTaken(damage));

            // Round the damage
            damage.Amount = decimal.Round(damage.Amount);

            // Take the damage
            Hp -= (int) damage.Amount;

            // Don't allow hp to become negative
            Hp = Hp < 0 ? 0 : Hp;

            // Allow effects to process damage taken
            Effects.ForEach(x => x.AfterDamageTaken(damage));
        }

        /// <summary>
        /// Handles before an action is performed
        /// </summary>
        public void BeforeActionPerformed()
        {
            // Allow effects to trigger before an action has been performed
            Effects.ForEach(x => x.BeforeActionPerformed());
        }

        /// <summary>
        /// Handles an action being performed
        /// </summary>
        public void ActionPerformed()
        {
            // Reset current charge
            CurrentCharge = 0;

            // Allow effects to trigger after an action has been performed
            Effects.ForEach(x => x.AfterActionPerformed());
        }

        /// <summary>
        /// Adds an effect to the character
        /// </summary>
        public void AddEffect(IEffect effect)
        {
            // Make sure we can add the effect
            if (!effect.CanApply())
                return;

            // Apply the effect
            effect.OnApply();
            Effects.Add(effect);
        }

        /// <summary>
        /// Removes an effect from the character
        /// </summary>
        public void RemoveEffect(IEffect effect)
        {
            // Process that the effect is being removed
            effect.OnRemove();

            // Remove the effect
            Effects.Remove(effect);
        }

        /// <summary>
        /// Gets an effect by its name
        /// </summary>
        public IEffect GetEffect(string name)
        {
            return Effects.FirstOrDefault(x => x.Name == name);
        }
    }
}
