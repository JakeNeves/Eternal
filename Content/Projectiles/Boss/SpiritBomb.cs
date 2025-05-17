using Microsoft.Xna.Framework;
using Eternal.Content.Dusts;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Audio;

namespace Eternal.Content.Projectiles.Boss
{
    public class SpiritBomb : ModProjectile
    {
        ref float BombTimer => ref Projectile.ai[1];

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            var entitySource = Projectile.GetSource_FromAI();

            BombTimer++;

            if (BombTimer >= 8f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                SoundEngine.PlaySound(SoundID.Item104, Projectile.position);

                if (Projectile.velocity.X != 0)
                {
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, 4f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, -4f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
                }

                if (Projectile.velocity.Y != 0)
                {
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(4f, 0f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-4f, 0f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
                }

                BombTimer = 0;
            }

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.UltraBrightTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.UltraBrightTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                SoundEngine.PlaySound(SoundID.Item100, Projectile.position);

                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(4f, 4f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(4f, -4f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-4f, 4f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-4f, -4f), ModContent.ProjectileType<SpiritBomb2>(), Projectile.damage / 2, 0);
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
    }
}
