using Eternal.Items.Weapons.Radiant;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories
{
    public class CometiteBattery : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Heavily increased ethereal power regeneration by 75%" + 
                             "\nProvides a large amount of ethereal power capacity 60");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 40;
            item.accessory = true;
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var modPlayer = RadiantPlayer.ModPlayer(player);
            modPlayer.etherealPowerMax2 += 60;
            modPlayer.etherealPowerRegenerationRate -= 0.75f;
        }
    }
}
