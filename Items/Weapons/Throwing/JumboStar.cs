using Eternal.Projectiles;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Throwing
{
    class JumboStar : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\nThrows an oversized four-point star");
        }
        public override void SetDefaults()
        {
            item.damage = 275;
            item.ranged = true;
            item.width = 186;
            item.height = 186;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 25;
            item.value = Item.buyPrice(platinum: 5);
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 3.5f;
            item.shoot = ProjectileType<JumboStarProjectile>();
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<Astragel>(), 15);
            recipe.AddIngredient(ItemType<StarmetalBar>(), 40);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
