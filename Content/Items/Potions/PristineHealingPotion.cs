using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Potions
{
    public class PristineHealingPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<Teal>();
            Item.healLife = 250;
            Item.potion = true;
            Item.value = Item.sellPrice(gold: 20);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>())
                .AddIngredient(ItemID.SuperHealingPotion)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
