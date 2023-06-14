using Eternal.Content.Items.Placeable;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Materials
{
    public class GalaciteGemstone : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 12);
            Item.maxStack = 9999;
        }
    }
}
