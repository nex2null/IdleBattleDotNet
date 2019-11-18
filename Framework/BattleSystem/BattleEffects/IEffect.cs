namespace Framework.BattleSystem.BattleEffects
{
    public interface IEffect
    {
        /// <summary>
        /// The character the effect is applied to
        /// </summary>
        BattleCharacter Character { get; set; }

        /// <summary>
        /// The name of the effect
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Whether the effect can be applied
        /// </summary>
        bool CanApply();

        /// <summary>
        /// Handles logic for when the effect is applied
        /// </summary>
        void OnApply();

        /// <summary>
        /// Handles logic for when the effect is removed
        /// </summary>
        void OnRemove();

        /// <summary>
        /// Handles logic for before a character action is performed
        /// </summary>
        void BeforeActionPerformed();

        /// <summary>
        /// Handles logic for after an action is performed
        /// </summary>
        void AfterActionPerformed();

        /// <summary>
        /// Handles logic for before damage is taken
        /// </summary>
        void BeforeDamageTaken(BattleDamage damage);

        /// <summary>
        /// Handles logic for after damage is taken
        /// </summary>
        void AfterDamageTaken(BattleDamage damage);
    }
}
