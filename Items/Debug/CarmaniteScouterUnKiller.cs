using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Debug
{
    class CarmaniteScouterUnKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carmanite Scouter Un-Killer");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\nMakes the Carmanite Scouter no longer a downed boss");
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
            if (EternalWorld.downedCarmaniteScouter)
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.Item4;
                item.useAnimation = 15;
                item.useTime = 15;
                EternalWorld.downedCarmaniteScouter = false;
                Main.NewText("Effects have been undone, kill the Carmanite Scouter again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
