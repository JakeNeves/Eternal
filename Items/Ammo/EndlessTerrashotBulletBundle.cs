using Eternal.Projectiles.Weapons.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Ammo
{
    public class EndlessTerrashotBulletBundle : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unlimited Ammo\nChases your enemies and can go through tiles");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.ranged = true;
            item.width = 24;
            item.height = 32;
            item.knockBack = 2f;
            item.rare = ItemRarityID.Yellow;
            item.shoot = ModContent.ProjectileType<TerrashotBulletProjectile>();
            item.shootSpeed = 16.75f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddIngredient(ModContent.ItemType<TerrashotBullet>(), 3996);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
