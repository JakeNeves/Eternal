using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    class EverfrostIcebourneSkeletalWings : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'You really thought these would shatter?'");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 36;
            item.value = Item.sellPrice(gold: 50);
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 210;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            maxCanAscendMultiplier = 2;
            maxAscentMultiplier = 4;
            constantAscend = 0.140f;
            ascentWhenFalling = 0.90f;
            ascentWhenRising = 0.20f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 10f;
            acceleration *= 4f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<SydaniteBar>(), 20);
            recipe.AddTile(TileType<Tiles.AncientForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
