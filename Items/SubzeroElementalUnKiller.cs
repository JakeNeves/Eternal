using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class SubzeroElementalUnKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Subzero Elemental Un-Killer");
            Tooltip.SetDefault("[c/FF0000:Cheat Item]\nMakes the Subzero Elemental no longer a downed boss\nkilling it will make the ice enemies spawn in the underworld");
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
                EternalWorld.downedSubzeroElemental = false;
                Main.NewText("Effects have been undone, kill the Subzero Elemental again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
