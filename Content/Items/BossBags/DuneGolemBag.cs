using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Hell;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.BossBags
{
    public class DuneGolemBag : ModItem
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

            if (DifficultySystem.hellMode)
            {
                player.QuickSpawnItem(entitySource, ModContent.ItemType<DuneEviscrator>());
            }

            player.QuickSpawnItem(entitySource, ModContent.ItemType<DuneCore>());

            player.QuickSpawnItem(entitySource, ModContent.ItemType<MachaliteShard>(), Main.rand.Next(60, 90));
        }

        public override int BossBagNPC => ModContent.NPCType<DuneGolem>();
    }
}
