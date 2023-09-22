using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class JakesHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 12;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ItemRarityID.White;
            Item.vanity = true;
        }
    }
}
