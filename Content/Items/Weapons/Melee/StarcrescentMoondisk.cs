using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class StarcrescentMoondisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Free the moonlight!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.noMelee = true;
            Item.damage = 1000;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<StarcrescentMoondiskProjectile>();
            Item.shootSpeed = 8.2f;
            Item.noUseGraphic = true;
            Item.knockBack = 3f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>())
                .AddIngredient(ModContent.ItemType<StargloomCometiteBar>(), 30)
                .AddIngredient(ModContent.ItemType<EmpoweredApparitionalDisk>())
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
