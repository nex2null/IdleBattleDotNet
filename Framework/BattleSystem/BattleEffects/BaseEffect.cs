namespace Framework.BattleSystem.BattleEffects
{
    public abstract class BaseEffect : IEffect
    {
        // Properties
        public BattleCharacter Character { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseEffect(BattleCharacter character, string name)
        {
            Character = character;
            Name = name;
        }

        /// <summary>
        /// Whether the effect can be applied
        /// </summary>
        public virtual bool CanApply()
        {
            return true;
        }

        /// <summary>
        /// Handles logic for when the effect is applied
        /// </summary>
        public virtual void OnApply()
        {
        }

        /// <summary>
        /// Handles logic for when the effect is removed
        /// </summary>
        public virtual void OnRemove()
        {
        }

        /// <summary>
        /// Handles logic for before a character action is performed
        /// </summary>
        public virtual void BeforeActionPerformed()
        {
        }

        /// <summary>
        /// Handles logic for after an action is performed
        /// </summary>
        public virtual void AfterActionPerformed()
        {
        }

        /// <summary>
        /// Handles logic for before damage is taken
        /// </summary>
        public virtual void BeforeDamageTaken(BattleDamage damage)
        {
        }

        /// <summary>
        /// Handles logic for after damage is taken
        /// </summary>
        public virtual void AfterDamageTaken(BattleDamage damage)
        {
        }
    }
}
