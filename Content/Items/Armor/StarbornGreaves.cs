using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class StarbornGreaves : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+18% increased movement speed");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.18f;
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 8);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 12);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 8);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 8)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 12)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 8)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
