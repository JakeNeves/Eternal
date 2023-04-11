using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class InfernalAshes : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'Think of this as hot sand'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 16;
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(gold: 5, silver: 90);
            Item.maxStack = 9999;
        }
    }
}
