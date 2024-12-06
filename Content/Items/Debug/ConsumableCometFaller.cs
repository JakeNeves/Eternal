using Eternal.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class ConsumableCometFaller : ModItem
    {
        public override string Texture => "Eternal/Content/Biomes/Comet_Icon";

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.rare = ItemRarityID.Gray;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 5;
            Item.useTime = 5;
            Item.consumable = true;
            Item.UseSound = SoundID.NPCDeath14;
        }

        public override bool? UseItem(Player player)
        {
            Main.NewText("A comet has landed and struck the world!", 0, 215, 215);
            CometSystem.DropComet();

            return base.UseItem(player);
        }
    }
}
