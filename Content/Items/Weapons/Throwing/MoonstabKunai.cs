using Eternal.Content.Projectiles.Weapons.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Throwing
{
    public class MoonstabKunai : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 24;
            Item.DamageType = DamageClass.Ranged;
            Item.rare = ItemRarityID.Red;
            Item.damage = 210;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<MoonstabKunaiProjectile>();
            Item.shootSpeed = 12f;
        }
    }
}
