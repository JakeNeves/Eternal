using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Tiles.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Ammo
{
    public class EndlessStarbulletBundle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Unlimited Ammo" +
                            "\n'Let's hope that these would work against anything...'");
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 12;
            Item.height = 21;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Red;
            Item.shoot = ModContent.ProjectileType<StarbulletProjectile>();
            Item.shootSpeed = 18f;
            Item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ModContent.ItemType<Starbullet>(), 3996)
                .Register();
        }
    }
}
