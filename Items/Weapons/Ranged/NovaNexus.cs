using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class NovaNexus : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'For when things get out of hand and start to cause chaos...'");
        }

        public override void SetDefaults()
        {
            item.width = 86;
            item.height = 32;
            item.damage = 900;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/NovaNexus");
            item.autoReuse = true;
            item.shootSpeed = 5f;
            item.shoot = AmmoID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.rare = ItemRarityID.Red;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 5;
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

    }
}
