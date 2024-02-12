using Eternal.Common.Players;
using Eternal.Content.Items.Placeable;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class BlackLantern : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Allows you to see while in The Beneath");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessorySystem.BlackLantern = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.Anvils)
                .AddIngredient(ItemID.Torch, 12)
                .AddIngredient(ModContent.ItemType<Gloomrock>(), 16)
                .Register();
        }
    }
}
