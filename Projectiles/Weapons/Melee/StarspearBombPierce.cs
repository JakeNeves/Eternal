using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class StarspearBombPierce : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starspear Bomb");
        }

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 300;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();
        }

        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCHit3, projectile.position);
        }
    }
}
