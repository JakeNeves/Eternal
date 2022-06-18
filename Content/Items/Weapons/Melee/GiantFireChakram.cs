using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class GiantFireChakram : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Pink;
            Item.damage = 100;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<GiantFireChakramProjectile>();
            Item.shootSpeed = 12f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }
    }
}
