using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Itemization;
using Framework.Itemization.Enums;
using Framework.Itemization.Equipment;

namespace Ui
{
    class Program
    {
        static void Main(string[] args)
        {
            DoBattleThings();
        }

        static void DoBattleThings()
        {
            Game.Instance.StartBattle(1).Wait();
        }

        static void GenerateUntilEscape()
        {
            Console.WriteLine("Press any key to generate an item. Escape exits.");
            Console.WriteLine("==========");
            Console.WriteLine();

            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                var items = new List<Item>();

                while (items.Count == 0)
                {
                    items = LootGenerator.GenerateLoot(1, new List<LootGenerationOption>
                    {
                        new LootGenerationOption(null, ItemSuperTypeEnum.Equipment, null, 1, 100)
                    });
                }

                WriteItemDebugString(items.Single());
                items.Clear();
            }
        }

        static void GenerateUntilPerfect()
        {
            Console.WriteLine("Generating an item until it's perfect. Wait for a long time...");
            Console.WriteLine("==========");
            Console.WriteLine();

            int count = 0;
            while (true)
            {
                count++;

                var items = LootGenerator.GenerateLoot(1, new List<LootGenerationOption>
                {
                    new LootGenerationOption(null, ItemSuperTypeEnum.Equipment, null, 1, 100)
                });

                if (!items.Any())
                    continue;

                var equipment = items.Single() as Equipment;
                if (equipment.Affixes.Count < 6)
                    continue;

                var perfectAffixes = true;
                foreach (var affix in equipment.Affixes)
                {
                    var affixInformation = ItemData.GetAffixInformation(affix.Type);
                    if (affix.Value != affixInformation.MaxValue)
                    {
                        perfectAffixes = false;
                        break;
                    }
                }

                var perfectImplicits = true;
                foreach (var equipImplicit in equipment.Implicits)
                {
                    var implicitInformation = ItemData.GetEquipmentImplicitInformation(equipment.Type).First(x => x.ModifiedStat == equipImplicit.Stat);
                    if (equipImplicit.Value != implicitInformation.MaxValue)
                    {
                        perfectImplicits = false;
                        break;
                    }
                }

                if (!perfectAffixes || !perfectImplicits)
                    continue;

                Console.Beep();
                WriteItemDebugString(equipment);
                break;
            }

            Console.WriteLine($"Took {count} tries to make a perfect item");
        }

        static void WriteItemDebugString(Item item)
        {
            var itemInformation = ItemData.GetItemInformation(item.Type);

            if (itemInformation.ItemSuperType == ItemSuperTypeEnum.Currency)
                Console.WriteLine(itemInformation.ItemName);

            if (itemInformation.ItemSuperType == ItemSuperTypeEnum.Equipment)
            {
                var equipment = item as Equipment;
                Console.WriteLine($"{equipment.Name} - {itemInformation.ItemName} - {equipment.Rarity}");

                foreach (var equipImplicit in equipment.Implicits)
                    Console.WriteLine($"\tImplicit - {GetImplicitDebugString(equipImplicit)}");

                foreach (var affix in equipment.Affixes)
                    Console.WriteLine($"\t{GetAffixDebugString(affix)}");
            }

            Console.WriteLine();
            Console.WriteLine("==========");
            Console.WriteLine();
        }

        static string GetImplicitDebugString(EquipmentImplicit equipImplicit)
        {
            return $"{equipImplicit.Stat} - {equipImplicit.Value}";
        }

        static string GetAffixDebugString(EquipmentAffix affix)
        {
            var affixInformation = ItemData.GetAffixInformation(affix.Type);
            return $"{affixInformation.Slot} - {affixInformation.ModifiedStat} - {affix.Value}";
        }
    }
}
