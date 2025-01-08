using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Common.Configurations;

namespace Eternal.Content.Items.Materials
{
    public class KnifeHandle : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return !ServerConfig.instance.update14;
        }

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 8;
            Item.rare = ItemRarityID.Gray;
        }
    }
}
