using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class DeadMeat : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 18;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 6);
            Item.maxStack = 9999;
        }
    }
}
