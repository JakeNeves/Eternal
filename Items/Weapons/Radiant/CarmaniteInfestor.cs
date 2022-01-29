using Eternal.Projectiles.Weapons.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Radiant
{
    public class CarmaniteInfestor : RadiantItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a Carmanite Parasite");
        }

        public override void SafeSetDefaults()
        {
            item.width = 36;
            item.height = 24;
            item.damage = 25;
            item.noMelee = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.knockBack = 2.4f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 6.2f;
            item.shoot = ModContent.ProjectileType<CarmaniteParasiteWeapon>();
            item.rare = ItemRarityID.Green;
            etherealPowerCost = 5;
        }
    }
}
