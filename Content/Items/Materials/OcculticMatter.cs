using Eternal.Common.Configurations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class OcculticMatter : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 2));
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 2);
            Item.rare = ItemRarityID.Pink;
            Item.maxStack = 9999;
        }
    }
}
