using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Debug
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
            if (EternalWorld.downedCosmicApparition)
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                EternalWorld.downedCosmicApparition = false;
                Main.NewText("Effects have been undone, kill the Cosmic Apparition again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
