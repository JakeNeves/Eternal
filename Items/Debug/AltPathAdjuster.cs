using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Debug
{
    public class AltPathAdjuster : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Alt-Path Adjuster");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\n<right> to enable Alt-Path Buffs\nLeft Click to disable Alt Path Buffs\nThis will affect Pre-Hardmode and Hardmode Depending on what boss you've defeated");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
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
                EternalWorld.downedCarmaniteScouter = true;
                EternalWorld.downedDunekeeper = true;
                EternalWorld.downedDroxClan = true;
                Main.NewText("Alt-Path Buffs have been applied", 0, 215, 215);
            }
            else
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                EternalWorld.downedCarmaniteScouter = false;
                EternalWorld.downedDunekeeper = false;
                EternalWorld.downedDroxClan = false;
                Main.NewText("Alt-Path Buffs have been removed", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
