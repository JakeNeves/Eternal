using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Eternal.Tiles;

namespace Eternal.Items
{
    class SiivaniteAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Contains Godly Energy\n'The power of the scourges linger within the energy'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 6));
        }

        public override void SetDefaults()
        {
            item.width = 29;
            item.height = 23;
            item.value = Item.buyPrice(platinum: 3);
            item.rare = ItemRarityID.Red;
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<SydaniteBar>());
            recipe.AddIngredient(ItemType<ThunderiteBar>());
            recipe.AddIngredient(ItemType<ScoriumBar>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
