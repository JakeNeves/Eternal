using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Expert
{
    public class DuneCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Causes sparks to fly upon geeting hit");
        }
        
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 42;
            Item.value = Item.sellPrice(silver: 25, copper: 15);
            Item.rare = ItemRarityID.Expert;
            Item.accessory = true;
            Item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AccessorySystem.DuneCore = true;
        }
    }
}
