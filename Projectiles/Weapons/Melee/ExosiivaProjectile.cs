using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Weapons.Melee
{
    public class ExosiivaProjectile : ModProjectile
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exosiiva Gladus Blade");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 102;
            projectile.height = 102;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 900;
            projectile.alpha = 0;
            projectile.light = 0.5f;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.Kill();

            projectile.alpha++;

            if (Eternal.instance.CalamityLoaded)
            {
                target.AddBuff(ModLoader.GetMod("CalamityMod").BuffType("ExoFreeze"), 240);
                target.AddBuff(ModLoader.GetMod("CalamityMod").BuffType("Vaporfied"), 240);
                target.AddBuff(ModLoader.GetMod("CalamityMod").BuffType("ProfanedWeakness"), 240);
                target.AddBuff(ModLoader.GetMod("CalamityMod").BuffType("GalvanicCorrosion"), 240);
            }
        }

        public override void AI()
        {
            projectile.rotation += projectile.velocity.X * 0.1f;

            if (projectile.timeLeft <= 50)
            {
                projectile.velocity.Y++;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Electric, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.NPCHit41, projectile.position);
        }

    }
}
