using Eternal.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Ranged
{
    public class CarmaniteInfestor : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a Carmanite Parasite");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 26;
            item.damage = 16;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 28;
            item.useAnimation = 28;
            item.knockBack = 2.4f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 6.2f;
            item.shoot = ProjectileType<CarmaniteParasiteWeapon>();
            item.rare = ItemRarityID.Green;
        }
    }
}
