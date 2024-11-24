using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Misc
{
    public class ChaosCardProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
            
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.Center + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

            target.life = 1;
            player.ApplyDamageToNPC(target, 9999, 0f, 0, false);

        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;

            Projectile.rotation += Projectile.velocity.X * 0.1f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.Center + Projectile.velocity, Projectile.width, Projectile.height, DustID.Wraith, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            return true;
        }
    }
}
