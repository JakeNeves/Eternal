using Eternal.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class IncineriusUnKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Incinerius Un-Killer");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\nMakes Incinerius no longer a downed boss\nkilling him will skip the Basalt Prison...");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Purple;
            Item.useAnimation = 15;
            Item.useTime = 15;
        }

        public override bool CanUseItem(Player player)
        {
            if (DownedBossSystem.downedIncinerius)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.UseSound = SoundID.Item4;
                Item.useAnimation = 15;
                Item.useTime = 15;
                DownedBossSystem.downedIncinerius = false;
                Main.NewText("Effects have been undone, kill Incinerius again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
