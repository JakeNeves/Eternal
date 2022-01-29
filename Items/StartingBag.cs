using Eternal.Items.Accessories;
using Eternal.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items
{
    public class StartingBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mysterious Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}\n'Whatever is in there, could be useful...'");
        }

        public override void SetDefaults()
        {
            item.consumable = true;
            item.width = 24;
            item.height = 32;
            item.rare = ItemRarityID.Blue;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            //The Creator's Message item will be temporarly here until the mod is complete.
            player.QuickSpawnItem(ItemType<CreatorsMessage>());
            player.QuickSpawnItem(ItemType<AncientEmblem>());
            player.QuickSpawnItem(ItemType<BloodLocket>());
        }

        public override int BossBagNPC => NPCType<NPCs.Boss.AoI.ArkofImperious>();
    }
}
