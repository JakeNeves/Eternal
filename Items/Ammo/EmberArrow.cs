using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Ranged;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Ammo
{
    public class EmberArrow : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 16;
            item.ranged = true;
            item.width = 14;
            item.height = 34;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 2f;
            item.rare = ItemRarityID.LightPurple;
            item.shoot = ProjectileType<EmberArrowProjectile>();
            item.shootSpeed = 8.5f;
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddIngredient(ItemType<ScorchedGrimstoneCompound>(), 2);
            recipe.SetResult(this, 16);
            recipe.AddRecipe();
        }

    }
}
