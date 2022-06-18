using Eternal.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Debug
{
    public class AoIUnKiller : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark of Imperious Un-Killer");
            Tooltip.SetDefault("[c/FF0000:Debug Item]\nMakes the Ark of Imperious no longer a downed boss\nkilling it will grant the ability to summon the Cosmic Emperor, via the Cosmic Tablet at the Altar of Cosmic Desire");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item4;
            Item.rare = ItemRarityID.Purple;
            Item.useAnimation = 15;
            Item.useTime = 15;
        }

        public override bool CanUseItem(Player player)
        {
            if (DownedBossSystem.downedArkofImperious)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.UseSound = SoundID.Item4;
                Item.useAnimation = 15;
                Item.useTime = 15;
                DownedBossSystem.downedArkofImperious = false;
                Main.NewText("Effects have been undone, kill the Ark of Imperious again to redo the effect...", 0, 215, 215);
            }
            return base.CanUseItem(player);
        }
    }
}
