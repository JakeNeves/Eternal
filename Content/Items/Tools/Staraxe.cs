using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Tools
{
    public class Staraxe : ModItem
    {

        public override void SetDefaults()
        {
            Item.tileBoost = 12;
            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.width = 64;
            Item.height = 64;
            Item.useTime = 10;
            Item.useAnimation = 15;
            Item.axe = 150;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 12;
            Item.value = Item.buyPrice(gold: 30, silver: 75);
            Item.rare = ModContent.RarityType<Teal>();
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 4)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 5)
                .AddIngredient(ModContent.ItemType<Astragel>(), 10)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
