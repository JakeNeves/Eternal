using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Ammo
{
    public class EndlessNexusBulletBundle : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unlimited Ammo\n'Let's hope that these would work against anything...'");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 20;
            item.height = 21;
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
            recipe.AddIngredient(ItemType<NexusBullet>(), 3996);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
