using System;

namespace Framework.BattleSystem.BattleEffects
{
    public class SlowedEffect : BaseEffect
    {
        // Properties
        public int TurnsRemaining { get; set; }
        public int ReducedSpeed { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public SlowedEffect(BattleCharacter character, int turnsToSlow)
            : base(character, "Slowed")
        {
            TurnsRemaining = turnsToSlow;
        }

        /// <summary>
        /// Whether the effect can be applied
        /// </summary>
        public override bool CanApply()
        {
            return Character.GetEffect(Name) == null;
        }

        /// <summary>
        /// Handles logic for when the effect is applied
        /// </summary>
        public override void OnApply()
        {
            // Remove 50% of the character's speed
            ReducedSpeed = (int) Math.Floor(Character.Spd * .5M);
            Character.Spd -= ReducedSpeed;
        }

        /// <summary>
        /// Handles logic for when the effect is removed
        /// </summary>
        public override void OnRemove()
        {
            // Give the character its speed back
            Character.Spd += ReducedSpeed;
        }

        /// <summary>
        /// Handles logic for before a character action is performed
        /// </summary>
        public override void BeforeActionPerformed()
        {
            // Decrement the turns remaining
            TurnsRemaining--;

            // If there are no more turns remaining remove the effect
            if (TurnsRemaining <= 0)
                Character.RemoveEffect(this);
        }
    }
}
