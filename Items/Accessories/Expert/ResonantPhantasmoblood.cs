using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Eternal.Items.Accessories.Expert
{
    public class ResonantPhantasmoblood : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows the player to turn invisible while standing still" +
                             "\nIncreased invinciblity frames");
        }

        public override void SetDefaults()
        {
            item.width = 25;
            item.height = 32;
            item.value = Item.buyPrice(platinum: 3);
            item.rare = ItemRarityID.Expert;
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.longInvince = true;
            player.shroomiteStealth = true;
        }
    }
}
