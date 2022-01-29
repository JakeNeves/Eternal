using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Magic;
using Eternal.Items.Weapons.Expert;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Items.Armor;
using Eternal.Items.Accessories.Expert;
using Eternal.Items.Materials;
using Eternal.Items.Accessories;

namespace Eternal.Items.BossBags
{
    public class EmprayniaBag : ModItem
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
                player.QuickSpawnItem(ItemType<NightmareReaperscythe>());
            }

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ItemType<Pyroyo>());
            }

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemType<Incinerator>());
            }

            player.QuickSpawnItem(ItemType<FlameInfusedJewel>());

            player.QuickSpawnItem(ItemType<ScorchedMetal>(), Main.rand.Next(25, 75));
        }

        public override int BossBagNPC => NPCType<NPCs.Boss.Incinerius.Incinerius>();
    }
}
