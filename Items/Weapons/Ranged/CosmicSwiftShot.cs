using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    class CosmicSwiftShot : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires arrows at extreme speeds\n'The Triple-Shot Bow of The Cosmos'");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 56;
            item.damage = 875;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 15.5f;
            item.shoot = AmmoID.Arrow;
            item.useAmmo = AmmoID.Arrow;
            item.rare = ItemRarityID.Red;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3;
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

    }
}
