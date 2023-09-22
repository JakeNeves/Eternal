using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class DuneMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
        }
    }
}
