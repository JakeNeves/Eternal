using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    class StormBeholder : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Rapidly Fires Arrows");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 56;
            item.damage = 60;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 15.5f;
            item.shoot = AmmoID.Arrow;
            item.useAmmo = AmmoID.Arrow;
            item.rare = ItemRarityID.Green;
        }
    }
}
