using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Ammo
{
    public class TerrashotBullet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Chases your enemies and can go through tiles");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.ranged = true;
            item.width = 14;
            item.height = 24;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.rare = ItemRarityID.Yellow;
            item.shoot = ModContent.ProjectileType<TerrashotBulletProjectile>();
            item.shootSpeed = 16.75f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.MusketBall);
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.SetResult(this, 333);
            recipe.AddRecipe();
        }

    }
}
