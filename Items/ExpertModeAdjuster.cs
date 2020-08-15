using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class ExpertModeAdjuster : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FF0000:Cheat Item]\n<right> to enable Expert Mode\nLeft Click to disable Expert Mode\nMake sure you turn off monster spawning before using this");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 30;
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
                Main.expertMode = true;
                Main.NewText("Expert Mode is Active", 0, 215, 215);
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                Main.expertMode = false;
                Main.NewText("Expert Mode is no longer Active", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
