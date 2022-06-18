using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Potions
{
    public class PerfectHealingPotion : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.healLife = 300;
            Item.potion = true;
            Item.value = Item.sellPrice(gold: 60);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>())
                .AddIngredient(ModContent.ItemType<PristineHealingPotion>())
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
