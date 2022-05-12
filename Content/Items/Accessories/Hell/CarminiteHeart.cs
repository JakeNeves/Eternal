using Eternal.Content.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Hell
{
    public class CarminiteHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heart of Carminite");
            Tooltip.SetDefault("Increased melee damage By 25%" +
                             "\nHell Mode drop");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = 0;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 0.25f;
        }
    }
}
