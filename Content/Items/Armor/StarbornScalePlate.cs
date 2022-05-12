using Eternal.Content.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class StarbornScalePlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+25 increased max life");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 42;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 50;
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 24);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 12);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/

    }
}
