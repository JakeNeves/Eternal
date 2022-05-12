using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class CarminiteDeadshot : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Feels fleshy...'");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 50;
            Item.damage = 22;
            Item.knockBack = 2.6f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 6f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
