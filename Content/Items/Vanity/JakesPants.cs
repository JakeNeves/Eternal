using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Vanity
{
    [AutoloadEquip(EquipType.Legs)]
    public class JakesPants : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.buyPrice(gold: 15);
            Item.rare = ItemRarityID.White;
            Item.vanity = true;
        }
    }
}
