using Eternal.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items
{
    class AncientCrystalineScrap : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("It's pulsing vibrance leads you to something, but where?");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 6));
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.buyPrice(gold: 5, silver: 75);
            item.rare = ItemRarityID.Orange;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AtomReconstructor>());
            recipe.AddIngredient(ItemType<AncientDust>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
