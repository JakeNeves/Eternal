using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Arrows
{
    public class StarbornArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Brittle, but tough!'");
        }

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 34;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.knockBack = 4f;
            Item.rare = ModContent.RarityType<Teal>();
            Item.shoot = ModContent.ProjectileType<StarbornArrowProjectile>();
            Item.shootSpeed = 20.5f;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe(333)
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 6)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 12)
                .Register();
        }
    }
}
