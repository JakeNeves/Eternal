using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;

namespace Eternal.Items
{
    class SydaniteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'It pulses as it gets colder'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 6));
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(gold: 5, silver: 90);
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Tiles.AncientForge>());
            recipe.AddIngredient(ItemType<Placeable.SydaniteOre>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
