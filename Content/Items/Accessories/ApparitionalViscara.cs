using Eternal.Content.Buffs;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class ApparitionalViscara : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("[c/F72F9A:Absolute RNG Drop]" +
                                                                            "\nIt does nothing for now");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.sellPrice(platinum: 15);
            Item.rare = ModContent.RarityType<AbsoluteRNG>();
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[ModContent.BuffType<Hypothermia>()] = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.Anvils)
                .AddIngredient(ItemID.Silk, 12)
                .AddIngredient(ItemID.FlinxFur, 16)
                .Register();
        }
    }
}
