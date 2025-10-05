using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Materials
{
    public class RawExathium : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Maroon>();
            Item.value = Item.sellPrice(gold: 10);
            Item.maxStack = 9999;
        }
    }
}
