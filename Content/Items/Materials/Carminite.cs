using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class Carminite : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Blood Droplets, Petrified and Bewiched in a bizzare form" +
                             "\n'Essance of Carminite Creatures'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 1);
            Item.maxStack = 999;
        }

    }
}
