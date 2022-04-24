using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories
{
    public class TyrantJuice : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("All offensive stats are increased" +
                             "\n'A bit of an alternative to the stimulants'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 30;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Pink;
            item.accessory = true;
            item.defense = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.03f;
            player.magicCrit += 1;
            player.meleeCrit += 1;
            player.rangedCrit += 1;
            player.minionKB += 0.5f;
            player.pickSpeed += 0.025f;
            player.moveSpeed += 0.075f;
            player.endurance += 0.03f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Bottles);
            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.IronskinPotion);
            recipe.AddIngredient(ItemID.Gel, 16);
            recipe.AddIngredient(ItemID.SoulofNight, 12);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
