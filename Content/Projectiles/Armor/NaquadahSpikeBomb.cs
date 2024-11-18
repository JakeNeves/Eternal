using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Armor
{
    public class NaquadahSpikeBomb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.aiStyle = 1;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0.75f, 0f, 0.75f);

            Projectile.rotation += (Projectile.velocity.X + Projectile.velocity.Y) * 0.1f;

            if (Projectile.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.frame = Main.rand.Next(0, 1);
                Projectile.ai[0] = 1f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 8; i++)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f)), ModContent.ProjectileType<NaquadahSpike>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }

            Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, 0f), ModContent.ProjectileType<NaquadahSpikeBombAOE>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 8; i++)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f)), ModContent.ProjectileType<NaquadahSpike>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }

            Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, 0f), ModContent.ProjectileType<NaquadahSpikeBombAOE>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
        }
    }
}
