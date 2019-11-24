using System;
using System.Collections.Generic;
using System.Text;
using Framework.Itemization;

namespace Framework
{
    public class Town
    {
        // Properties
        public int TotalExperience { get; set; }
        public int TotalGold { get; set; }
        public Inventory Inventory { get; set; }
        public List<PlayerCharacter> PlayerCharacters { get; set; }

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
    }
}
