using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Debug
{
    public class InvasionStopper : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Invasion Event Stopper");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\nStops any Invasions\nDOES NOT EFFECT VANILLA AND/OR OTHER MODDED INVASIONS");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 36;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item4;
            item.rare = ItemRarityID.Purple;
            item.useAnimation = 15;
            item.useTime = 15;
        }

        public override bool CanUseItem(Player player)
        {
            if (DroxClanWorld.DClan)
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                DroxClanWorld.DClan = false;
                Main.LocalPlayer.GetModPlayer<EternalPlayer>().droxEvent = false;
                Main.NewText("Effects have been undone, trigger any invasion using any invasion summoning item", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
