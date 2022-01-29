using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories.Expert
{
    public class FlameInfusedJewel : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lava Infused Crystal");
            Tooltip.SetDefault("Immunity to Lava");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 36;
			item.rare = ItemRarityID.Expert;
			item.expert = true;
			item.value = Item.sellPrice(gold: 5);
            		item.accessory = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lavaImmune = true;
            player.lavaMax = 14;
            player.lavaTime = 14;
        }
    }
}
