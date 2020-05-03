using Framework.Itemization;
using System;
using System.Collections.Generic;

namespace Framework
{
    public class Town
    {
        // Properties
        public int TotalExperience { get; private set; }
        public int TotalGold { get; private set; }
        public Inventory Inventory { get; private set; }
        public List<PlayerCharacter> PlayerCharacters { get; private set; }

        // Events
        public event Action TownUpdate;

        /// <summary>
        /// Constructor
        /// </summary>
        public Town()
        {
            TotalExperience = 0;
            TotalGold = 0;
            Inventory = new Inventory();
            PlayerCharacters = new List<PlayerCharacter>();

            PlayerCharacters.Add(new PlayerCharacter
            {
                Name = "Brian",
                Level = 1,
                Hp = 175,
                Mp = 0,
                Str = 9,
                Int = 1,
                Spd = 5
            });

            PlayerCharacters.Add(new PlayerCharacter
            {
                Name = "Chris",
                Level = 1,
                Hp = 250,
                Mp = 0,
                Str = 32,
                Int = 1,
                Spd = 2
            });
        }

        /// <summary>
        /// Updates the town's gold by a given delta amount. Delta can be either positive or negative.
        /// </summary>
        public void UpdateGold(int delta)
        {
            TotalGold += delta;
            TownUpdate?.Invoke();
        }

        /// <summary>
        /// Updates the town's experience by a given delta amount. Delta can be either positive or negative.
        /// </summary>
        /// <param name="delta"></param>
        public void UpdateExperience(int delta)
        {
            TotalExperience += delta;
            TownUpdate?.Invoke();
        }
    }
}
