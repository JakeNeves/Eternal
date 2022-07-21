using Eternal.Common.Players;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Expert
{
    public class Bloodtooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Causes bloodteeth to spew out upon getting hit.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(silver: 25, copper: 15);
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessorySystem.Bloodtooth = true;
        }
    }
}
