using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Common.Configurations;

namespace Eternal.Content.Items.Materials
{
    public class GehennaKeyFragment2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 10;
            Item.rare = ItemRarityID.Gray;
        }
    }
}
