using Eternal.Items.Accessories.Expert;
using Eternal.Items.Materials.Elementalblights;
using Eternal.Items.Placeable;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Radiant;
using Eternal.Items.Weapons.Summon;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.BossBags
{
    public class SubzeroElementalBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = -12;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ItemType<FrostblightCrystal>(), Main.rand.Next(20, 40));

            if (Main.rand.Next(1) == 0)
            {
                player.QuickSpawnItem(ItemType<TheKelvinator>());
            }

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ItemType<FrostGladiator>());
            }

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemType<FrostyImmaterializer>());
            }

            if (NPC.downedMoonlord)
            {
                player.QuickSpawnItem(ItemType<SydaniteOre>(), Main.rand.Next(20, 60));

                if (Main.rand.Next(4) == 0)
                {
                    player.QuickSpawnItem(ItemType<Frostpike>());
                }

                if (Main.rand.Next(5) == 0)
                {
                    player.QuickSpawnItem(ItemType<FrostDisk>());
                }

                if (Main.rand.Next(6) == 0)
                {
                    player.QuickSpawnItem(ItemType<BaneofKelvin>());
                }
            }

            player.QuickSpawnItem(ItemType<FrostKingsCore>());
        }

        public override int BossBagNPC => NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>();
    }
}
