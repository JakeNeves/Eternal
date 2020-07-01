using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace Eternal.Items
{
    class SpiritualGem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A gem imbued with ancient dust and light and dark shards and souls");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 6));
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 14;
            item.value = Item.buyPrice(gold: 25, silver: 50);
            item.rare = 8;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AtomReconstructor>());
            recipe.AddIngredient(ItemID.SoulofLight, 1);
            recipe.AddIngredient(ItemID.SoulofNight, 1);
            recipe.AddIngredient(ItemID.LightShard, 1);
            recipe.AddIngredient(ItemID.DarkShard, 1);
            recipe.AddIngredient(ItemType<AncientDust>(), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
