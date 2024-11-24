using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class TheEclipseOrbit : ModProjectile
    {
        int shootTimer;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.height = 26;
            Projectile.width = 26;

            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 1f;

            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            shootTimer++;

            var entitySource = Projectile.GetSource_FromAI();

            if (shootTimer >= 250)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center.X, Projectile.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<TheEclipseShoot>(), Projectile.damage, 0, Main.myPlayer, 0f, 0f);
                shootTimer = 0;
            }

            Projectile proj = Main.projectile[(int)Projectile.localAI[0]];

            Projectile.timeLeft++;
            Projectile.rotation += 0.1f;

            if (!proj.active || proj.type != ModContent.ProjectileType<TheEclipseProjectile>() || proj.owner != Projectile.owner)
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.owner == Main.myPlayer)
            {
                //rotating mf
                float distanceFromPlayer = 40;

                Projectile.position = proj.Center + new Vector2(distanceFromPlayer, 0f).RotatedBy(Projectile.ai[1]);
                Projectile.position.X -= Projectile.width / 2;
                Projectile.position.Y -= Projectile.height / 2;

                float rotation = (float)Math.PI / 60;
                Projectile.ai[1] += rotation;
                if (Projectile.ai[1] > (float)Math.PI)
                {
                    Projectile.ai[1] -= 2f * (float)Math.PI;
                    Projectile.netUpdate = true;
                }
            }

            Projectile.damage = proj.damage;
            Projectile.knockBack = proj.knockBack;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 1024); // placeholder buff
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
