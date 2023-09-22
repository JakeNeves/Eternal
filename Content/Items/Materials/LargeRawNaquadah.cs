using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Materials
{
    public class LargeRawNaquadah : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Otherworldly substance, mostly found in an unknown rift...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.value = Item.sellPrice(platinum: 10);
            Item.maxStack = 9999;
        }
    }
}
