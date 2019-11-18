namespace Framework.BattleSystem
{
    public static class BattleFormulas
    {
        /// <summary>
        /// Calculates if an attacker will hit a defender
        /// </summary>
        public static bool CalculateHit(BattleCharacter attacker, BattleCharacter defender)
        {
            //   // The base miss % is 5%
            //   var baseMissPercent = 5;

            //   // Level difference adds to the percent
            //   var levelModifier = (defender.level - attacker.level) * .5;
            //   var missPercent = baseMissPercent + levelModifier;

            //   // The speed of the defender can add an additional 10%
            //   var evasionModifier = (defender.spd / 100) * 10;
            //   missPercent += evasionModifier;

            //   // Roll a number 0 to 100 and if it is less than the miss percent the attack misses
            //   var roll = Math.random() * 100;
            //   return roll > Math.round(missPercent);

            return true;
        }

        /// <summary>
        /// Processes damage that would be dealt by an attacker to a defender
        /// </summary>
        public static BattleDamage ProcessDamage(
            BattleCharacter attacker,
            BattleCharacter defender,
            BattleDamage baseDamage)
        {
            // TODO: Things
            return baseDamage;
        }
    }
}
