using Eternal.Content.Items.Materials;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.BossBags
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
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = -12;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            var entitySource = player.GetSource_OpenItem(Type);

            player.QuickSpawnItem(entitySource, ModContent.ItemType<ApparitionalMatter>(), Main.rand.Next(30, 60));
        }

        public override int BossBagNPC => ModContent.NPCType<CosmicApparition>();
    }
}
