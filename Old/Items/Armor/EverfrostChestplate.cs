using Eternal.Items.Materials;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    class EverfrostChestplate : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = Item.sellPrice(gold: 30);
            item.rare = ItemRarityID.Red;
            item.defense = 30;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<SydaniteBar>(), 30);
            recipe.AddTile(TileType<AncientForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
