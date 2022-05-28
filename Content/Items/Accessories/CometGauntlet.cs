using Eternal.Content.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories
{
    public class CometGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("60% increased melee damage" +
                               "\n17% increased melee speed" +
                               "\nEnables auto swing for melee weapons" +
                               "\n'The comets are now in the palm of your hand'");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ModContent.RarityType<Teal>();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //EternalGlobalProjectile.cometGauntlet = true;

            player.GetDamage(DamageClass.Melee) += 0.6f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.17f;
            player.autoReuseGlove = true;
        }
    }
}
