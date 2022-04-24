using Eternal.Items.Accessories.Expert;
using Eternal.Items.Materials;
using Eternal.Items.Weapons.Magic;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.BossBags
{
    public class CosmicApparitionBag : ModItem
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
                player.QuickSpawnItem(ItemType<ApparitionalRendingStaff>());
            }

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ItemType<Vexation>());
            }

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemType<Cometstorm>());
            }

            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(ItemType<GhoulishGladius>());
            }

            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(ItemType<CosmicGhostbuster>());
            }

            player.QuickSpawnItem(ItemType<ResonantPhantasmoblood>());
            player.QuickSpawnItem(ItemType<ApparitionalMatter>(), Main.rand.Next(30, 80));
        }

        public override int BossBagNPC => NPCType<NPCs.Boss.CosmicApparition.CosmicApparition>();
    }
}
