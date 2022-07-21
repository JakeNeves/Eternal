using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class InterstellarMetal : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'It pulses very faintly'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.value = Item.sellPrice(platinum: 10);
            Item.maxStack = 999;
        }
    }
}
