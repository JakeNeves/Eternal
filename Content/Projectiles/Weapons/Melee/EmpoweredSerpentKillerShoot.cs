using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class EmpoweredSerpentKillerShoot : ModProjectile
    {
        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Empowered Serpent Killer");

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            for (int i = 0; i < 50; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 30;
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.Shadowflame);
                dust.noGravity = false;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }

            for (int i = 0; i < 50; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 60;
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.DemonTorch);
                dust.noGravity = false;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 8;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }

            Projectile.Kill();
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;

            Projectile.rotation += Projectile.velocity.X * 0.1f;

            if (!justSpawned)
            {
                SoundEngine.PlaySound(SoundID.NPCHit3, Projectile.position);

                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.Shadowflame);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                    Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.DemonTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - Projectile.Center) * 2;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }

                justSpawned = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath3, Projectile.position);

            for (int i = 0; i < 25; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 12.5f * i)) * 15;
                Dust dust = Dust.NewDustPerfect(Projectile.position, DustID.PurpleTorch);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 2;
                dust.noLight = false;
                dust.fadeIn = 1f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}
