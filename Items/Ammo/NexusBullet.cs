using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Ammo
{
    public class NexusBullet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Let's hope that these would work against anything...'");
        }

        public override void SetDefaults()
        {
            item.damage = 225;
            item.ranged = true;
            item.width = 12;
            item.height = 21;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<NexusBulletProjectile>();
            item.shootSpeed = 18f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<StarmetalBar>());
            recipe.AddIngredient(ItemType<GalaxianPlating>());
            recipe.AddIngredient(ItemType<InterstellarSingularity>());
            recipe.AddIngredient(ItemType<Astragel>());
            recipe.SetResult(this, 333);
            recipe.AddRecipe();
        }

    }
}
