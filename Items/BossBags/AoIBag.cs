using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.Weapons.Expert;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Items.Ammo;
using Eternal.Items.Materials;

namespace Eternal.Items.BossBags
{
    public class AoIBag : ModItem
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
            if (Main.rand.Next(1) == 0)
            {
                player.QuickSpawnItem(ItemType<Arkbow>());
                player.QuickSpawnItem(ItemType<ArkArrow>(), Main.rand.Next(300, 900));
            }

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ItemType<TheImperiousCohort>());
            }

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemType<DormantHeroSword>());
            }

            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(ItemType<TheEnigma>());
            }

            if (EternalWorld.hellMode)
                player.QuickSpawnItem(ItemType<ArkaniumCompound>(), Main.rand.Next(60, 80));
            else
                player.QuickSpawnItem(ItemType<ArkaniumCompound>(), Main.rand.Next(40, 60));
        }

        public override int BossBagNPC => NPCType<NPCs.Boss.AoI.ArkofImperious>();
    }
}
