using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class DawnstoneChestplate : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.LightPurple;
            item.defense = 16;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<Dawnstone>(), 30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
