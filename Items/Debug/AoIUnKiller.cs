using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Debug
{
    public class AoIUnKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark of Imperious Un-Killer");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\nMakes the Ark of Imperious no longer a downed boss\nkilling it will summon the cosmic emperor (NYI) via the Cosmic Tablet again...");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 50;
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
                EternalWorld.downedArkOfImperious = false;
                Main.NewText("Effects have been undone, kill the Ark of Imperious again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
