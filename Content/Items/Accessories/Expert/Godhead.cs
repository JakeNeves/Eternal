using Eternal.Common.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Expert
{
    public class Godhead : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            Item.value = Item.sellPrice(gold: 3, platinum: 1);
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessorySystem.Godhead = true;
        }
    }
}
