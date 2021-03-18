using Eternal.Projectiles.Weapons.Expert;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Expert
{
    class Bloodtooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\n'Very meaty, very meaty indeed...'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.rare = -12;
            item.damage = 24;
            item.knockBack = 10.5f;
            item.expert = true;
            item.ranged = true;
            item.noMelee = true;
            item.shootSpeed = 5.5f;
            item.shoot = ProjectileType<BloodtoothProjectile>();
            item.consumable = false;
            item.autoReuse = true;
            item.noUseGraphic = true;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
        }
    }
}
