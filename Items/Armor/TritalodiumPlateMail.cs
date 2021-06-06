using Eternal.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    class TritalodiumPlateMail : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 20;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Green;
            item.defense = 15;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 50;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<TritalodiumBar>(), 30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
