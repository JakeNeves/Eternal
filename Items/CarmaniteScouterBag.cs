using Eternal.Items.Weapons.Expert;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items
{
    class CarmaniteScouterBag : ModItem
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
            player.QuickSpawnItem(ItemType<Bloodtooth>());
            player.QuickSpawnItem(ItemType<TritalodiumBar>(), 30);
            player.QuickSpawnItem(ItemType<Carmanite>(), 25);
        }

        public override int BossBagNPC => NPCType<NPCs.Boss.CarmaniteScouter>();
    }
}
