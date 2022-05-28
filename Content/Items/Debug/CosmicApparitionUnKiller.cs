using Eternal.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class CosmicApparitionUnKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Apparition Un-Killer");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\nMakes the Cosmic Apparition no longer a downed boss\nkilling it will make enemies in the comet biome drop cometite materials again...");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Purple;
            Item.useAnimation = 15;
            Item.useTime = 15;
        }

        public override bool CanUseItem(Player player)
        {
            if (DownedBossSystem.downedCosmicApparition)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.UseSound = SoundID.Item4;
                Item.useAnimation = 15;
                Item.useTime = 15;
                DownedBossSystem.downedCosmicApparition = false;
                Main.NewText("Effects have been undone, kill the Cosmic Apparition again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
