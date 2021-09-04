using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Ammo
{
    public class ScatterBullet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Explodes into eight upon impact");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.ranged = true;
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 1.5f;
            item.rare = ItemRarityID.Lime;
            item.shoot = ProjectileType<ScatterBulletProjectile>();
            item.shootSpeed = 18f;
            item.ammo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemType<DroxPlate>(), 2);
            recipe.AddIngredient(ItemType<DroxCore>());
            recipe.SetResult(this, 333);
            recipe.AddRecipe();
        }

    }
}
