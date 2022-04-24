using Eternal.Items.Materials.Elementalblights;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    public class FierceDeityEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased damage\n[c/008060:Ancient Artifact] \nAn emblem empowered with a godly radiance\nLegends say this was used to empower the guardians of the dunes, aiding them in combat and allowing them to become reststant to electrical contact");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(gold: 13, silver: 70);
            item.rare = ItemRarityID.Lime;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.allDamage += 0.20f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemType<CarnageblightCrystal>(), 12);
            recipe.AddIngredient(ItemType<DuskblightCrystal>(), 6);
            recipe.AddIngredient(ItemID.DestroyerEmblem);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
