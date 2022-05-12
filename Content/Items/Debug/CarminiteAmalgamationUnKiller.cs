using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Common.Systems;

namespace Eternal.Content.Items.Debug
{
    public class CarminiteAmalgamationUnKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carminite Amalgamation Un-Killer");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\nMakes the Carminite Amalgamation no longer a downed boss\nKilling it again will make it spawn Iesnium Ore");
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Purple;
            Item.useAnimation = 15;
            Item.useTime = 15;
        }

        public override bool CanUseItem(Player player)
        {
            if (DownedBossSystem.downedCarminiteAmalgamation)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.UseSound = SoundID.Item4;
                Item.useAnimation = 15;
                Item.useTime = 15;
                DownedBossSystem.downedCarminiteAmalgamation = false;
                Main.NewText("Effects have been undone, kill the Carminite Amalgamation again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
