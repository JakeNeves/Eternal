using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Vanity
{
    public class SpiritArkCursor : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Causes your mouse cursor to shift from various shades of green");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.accessory = true;
            Item.vanity = true;
        }

        public override void UpdateVanity(Player player)
        {
            AccessorySystem.hasSpiritArkCursor = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<VoidCursor>())
                .AddIngredient(ModContent.ItemType<ArkiumQuartzPlating>())
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
        }
    }
}
