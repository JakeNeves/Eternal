using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class PostMoonLordAdjuster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Post-Moon Lord Adjuster");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\n<right> to enable post-moon lord\nLeft Click to disable post-moon lord");
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.useAnimation = 15;
            Item.useTime = 15;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.UseSound = SoundID.Item4;
                Item.useAnimation = 15;
                Item.useTime = 15;
                NPC.downedMoonlord = true;
                Main.NewText("Cosmic entities are roaming in the night sky", 0, 215, 215);
            }
            else
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.UseSound = SoundID.Item4;
                Item.useAnimation = 15;
                Item.useTime = 15;
                NPC.downedMoonlord = false;
                Main.NewText("Cosmic entities are no longer roaming in the night sky", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
