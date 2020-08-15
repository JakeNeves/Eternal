using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class HardmodeAdjuster : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FF0000:Cheat Item]\n<right> to enable hardmode\nLeft Click to disable hardmode\nMake sure you turn off monster spawning before using this");
        }

        public override void SetDefaults()
        {
            item.width = 30;
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
                Main.hardMode = true;
                Main.NewText("Hardmode is Active", 0, 215, 215);
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                Main.hardMode = false;
                Main.NewText("Hardmode is No Longer Active", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
