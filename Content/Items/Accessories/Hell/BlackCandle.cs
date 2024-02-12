using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Placeable;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Hell
{
    public class BlackCandle : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Allows you to see while in The Beneath" +
                                                                            "\nProvides a brighter source of light" +
                                                                            "\nHell Mode upgrade");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ModContent.RarityType<HellMode>();
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessorySystem.BlackCandle = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.Anvils)
                .AddIngredient(ModContent.ItemType<ShadowSkull>())
                .AddIngredient(ModContent.ItemType<BlackLantern>())
                .AddIngredient(ModContent.ItemType<Gloomrock>(), 36)
                .AddIngredient(ItemID.Candle)
                .Register();
        }
    }
}
