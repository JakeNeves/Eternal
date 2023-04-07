using Eternal.Content.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class InfernoblightCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'A mere fragment of the underworld's creatures'");

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 34;
            Item.value = Item.buyPrice(platinum: 1);
            Item.rare = ModContent.RarityType<Teal>();
            Item.maxStack = 999;
        }
    }
}
