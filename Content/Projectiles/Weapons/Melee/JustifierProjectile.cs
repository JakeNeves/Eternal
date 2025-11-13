using Eternal.Content.Buffs.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class JustifierProjectile : ModProjectile
    {
        Vector2 CircleDirc = new Vector2(0.0f, 15f);

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 56;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 3;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.WhiteTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f, newColor: Main.DiscoColor);

            var entitySource = Projectile.GetSource_FromAI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_FromThis();

            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            if (Main.rand.NextBool(8))
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        CircleDirc = Utils.RotatedBy(CircleDirc, 0.60f, new Vector2());
                        int index5 = Projectile.NewProjectile(entitySource, Projectile.Center, CircleDirc, ModContent.ProjectileType<JustifierProjectile2>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        int index6 = Projectile.NewProjectile(entitySource, Projectile.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<JustifierProjectile2>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                        Main.projectile[index5].timeLeft = 60;
                        Main.projectile[index6].timeLeft = 60;
                    }

                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 15 * i)) * 30;
                        Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.WhiteTorch, newColor: Main.DiscoColor);
                        dust.noGravity = true;
                        dust.velocity = Vector2.Normalize(position - Projectile.Center) * 4;
                        dust.noLight = false;
                        dust.fadeIn = 1f;
                    }
                }
            }

            if (Main.rand.NextBool(4))
                target.AddBuff(ModContent.BuffType<ImmenseArmorFracture>(), 20 * 60);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/Projectiles/Weapons/Melee/JustifierProjectile_Shadow").Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, Main.DiscoColor, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }
    }
}
