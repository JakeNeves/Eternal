using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Eternal.Tiles;

namespace Eternal.Items.Materials
{
    public class SiivaniteAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Contains Godly Energy\n'The power of the primordial elementals linger within such divine material'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 6));
        }

        public override void SetDefaults()
        {
            item.width = 29;
            item.height = 23;
            item.value = Item.buyPrice(platinum: 1);
            item.rare = ItemRarityID.Red;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<SydaniteBar>());
            recipe.AddIngredient(ItemType<ThunderiteBar>());
            recipe.AddIngredient(ItemType<ScoriumBar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
