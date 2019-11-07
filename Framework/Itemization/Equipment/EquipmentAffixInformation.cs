using System.Collections.Generic;
using Framework.Enums;
using Framework.Itemization.Enums;

namespace Framework.Itemization.Equipment
{
    public class EquipmentAffixInformation
    {
        // Properties
        public EquipmentAffixSlotEnum Slot { get; private set; }
        public EquipmentAffixTypeEnum Type { get; private set; }
        public int RequiredLevel { get; private set; }
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }
        public StatEnum ModifiedStat { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public EquipmentAffixInformation(
            EquipmentAffixSlotEnum slot,
            EquipmentAffixTypeEnum type,
            int requiredLevel,
            int minValue,
            int maxValue,
            StatEnum modifiedStat)
        {
            Slot = slot;
            Type = type;
            RequiredLevel = requiredLevel;
            MinValue = minValue;
            MaxValue = maxValue;
            ModifiedStat = modifiedStat;
        }

        /// <summary>
        /// Builds a list of all equipment affix informations
        /// </summary>
        public static List<EquipmentAffixInformation> BuildEquipmentAffixInformations()
        {
            // Helpful vars
            var pfx = EquipmentAffixSlotEnum.Prefix;
            var sfx = EquipmentAffixSlotEnum.Suffix;

            return new List<EquipmentAffixInformation>
            {
                // Physical Power Prefixes
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Rusty, 1, 1, 10, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Heavy, 10, 11, 20, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Sharpened, 19, 21, 30, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Serrated, 27, 31, 40, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Wicked, 34, 41, 50, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Vicious, 40, 51, 60, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Bloodthirsty, 45, 61, 70, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Cruel, 50, 71, 80, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Tyrranical, 55, 81, 90, StatEnum.PhysicalPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Merciless, 60, 91, 100, StatEnum.PhysicalPower),

                // Cold Power Prefixes
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Frosted, 1, 1, 10, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Chilled, 10, 11, 20, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Icy, 19, 21, 30, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Arctic, 27, 31, 40, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Frigid, 34, 41, 50, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Freezing, 40, 51, 60, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Frozen, 45, 61, 70, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Glaciated, 50, 71, 80, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Polar, 55, 81, 90, StatEnum.ColdPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Numbing, 60, 91, 100, StatEnum.ColdPower),

                // Fire Power Prefixes
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Heated, 1, 1, 10, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Smoldering, 10, 11, 20, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Smoking, 19, 21, 30, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Sizzling, 27, 31, 40, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Burning, 34, 41, 50, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Flaming, 40, 51, 60, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Scorching, 45, 61, 70, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Incinerating, 50, 71, 80, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Blasting, 55, 81, 90, StatEnum.FirePower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Cremating, 60, 91, 100, StatEnum.FirePower),

                // Lightning Power Prefixes
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Humming, 1, 1, 10, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Buzzing, 10, 11, 20, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Snapping, 19, 21, 30, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Popping, 27, 31, 40, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Crackling, 34, 41, 50, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Sparking, 40, 51, 60, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Arcing, 45, 61, 70, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Shocking, 50, 71, 80, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Discharging, 55, 81, 90, StatEnum.LightningPower),
                new EquipmentAffixInformation(pfx, EquipmentAffixTypeEnum.Electrocuting, 60, 91, 100, StatEnum.LightningPower),

                // Physical Resistance Suffixes
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Rock, 1, 1, 5, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Stone, 10, 6, 10, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Iron, 19, 11, 15, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Steel, 27, 16, 20, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Barriers, 34, 21, 25, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Garrison, 40, 26, 30, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Fortress, 45, 31, 35, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Castle, 50, 36, 40, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Mountain, 55, 41, 45, StatEnum.PhysicalResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Earth, 60, 46, 50, StatEnum.PhysicalResistance),

                // Cold Resistance Suffixes
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Snow, 1, 1, 5, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Hale, 10, 6, 10, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Rime, 19, 11, 15, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Yeti, 27, 16, 20, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Blizzard, 34, 21, 25, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Avalanche, 40, 26, 30, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Tundra, 45, 31, 35, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Winter, 50, 36, 40, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Hibernation, 55, 41, 45, StatEnum.ColdResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.AbsoluteZero, 60, 46, 50, StatEnum.ColdResistance),

                // Fire Resistance Suffixes
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Kindling, 1, 1, 5, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Embers, 10, 6, 10, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Drake, 19, 11, 15, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Ashes, 27, 16, 20, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Cinders, 34, 21, 25, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Furnace, 40, 26, 30, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Magma, 45, 31, 35, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Inferno, 50, 36, 40, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Volcano, 55, 41, 45, StatEnum.FireResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Nova, 60, 46, 50, StatEnum.FireResistance),

                // Lightning Resistance Suffixes
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Clouds, 1, 1, 5, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Squall, 10, 6, 10, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Thunder, 19, 11, 15, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Lightning, 27, 16, 20, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Bolts, 34, 21, 25, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Gale, 40, 26, 30, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Tempest, 45, 31, 35, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Tornado, 50, 36, 40, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Vortex, 55, 41, 45, StatEnum.LightningResistance),
                new EquipmentAffixInformation(sfx, EquipmentAffixTypeEnum.Maelstrom, 60, 46, 50, StatEnum.LightningResistance),
            };
        }
    }
}