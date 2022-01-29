using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Eternal.Items.Materials.Elementalblights;

namespace Eternal.Items.Materials
{
    public class ThunderiteBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Zap em' real good!'");
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
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.AddIngredient(ModContent.ItemType<Placeable.ThunderiteOre>());
            recipe.AddIngredient(ModContent.ItemType<ThunderblightCrystal>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
