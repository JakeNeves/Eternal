using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AncientStarbornScalePlate : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10 increased max life");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 21;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 10;
        }
    }
}
