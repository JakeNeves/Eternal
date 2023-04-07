using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class ApparitionalMatter : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Paranormal energy emmits from it's very core'");

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(platinum: 1, gold: 30);
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }
    }
}
