using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class FierceDeityEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased damage" +
                             "\n[c/008060:Ancient Artifact]" +
                             "\nAn emblem empowered with a godly radiance" +
                             "\nLegends say this was used to empower the guardians of the dunes, aiding them in combat and allowing them to become reststant to electrical contact");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(gold: 13, silver: 70);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) += 0.20f;
        }
    }
}
