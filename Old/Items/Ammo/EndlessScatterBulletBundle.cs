using Eternal.Projectiles.Weapons.Ranged;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Ammo
{
    public class EndlessScatterBulletBundle : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unlimited Ammo\nExplodes into eight upon impact");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.ranged = true;
            item.width = 32;
            item.height = 32;
            item.knockBack = 1.5f;
            item.rare = ItemRarityID.Lime;
            item.shoot = ProjectileType<ScatterBulletProjectile>();
            item.shootSpeed = 18f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddIngredient(ItemType<ScatterBullet>(), 3996);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
