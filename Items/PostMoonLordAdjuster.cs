using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class PostMoonLordAdjuster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Post-Moon Lord Adjuster");
            Tooltip.SetDefault("[c/FF0000:Cheat Item]\n<right> to enable post-moon lord\nLeft Click to disable post-moon lord\nMake sure you turn off monster spawning before using this");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 36;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item4;
            item.useAnimation = 15;
            item.useTime = 15;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                NPC.downedMoonlord = true;
                Main.NewText("Cosmic entities are roaming in the night sky", 0, 215, 215);
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                NPC.downedMoonlord = false;
                Main.NewText("Cosmic entities are no longer roaming in the night sky", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
