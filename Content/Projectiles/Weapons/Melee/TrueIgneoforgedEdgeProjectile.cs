using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class TrueIgneoforgedEdgeProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.AllowsContactDamageFromJellyfish[Type] = true;
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 6;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.ownerHitCheckDistance = 300f;
            Projectile.usesOwnerMeleeHitCD = true;

            Projectile.stopsDealingDamageAfterPenetrateHits = true;

            Projectile.aiStyle = -1;

            Projectile.noEnchantmentVisuals = true;
        }

        public override void AI()
        {
            float num = 50f;
            float num2 = 15f;
            float num3 = Projectile.ai[1] + num;
            float num4 = num3 + num2;
            float num5 = 77f;

            if (Projectile.localAI[0] == 0f)
                SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, Projectile.position);

            Projectile.localAI[0] += 2f;

            Projectile.Opacity = Utils.Remap(Projectile.localAI[0], 0f, Projectile.ai[1], 0f, 1f) * Utils.Remap(Projectile.localAI[0], num3, num4, 1f, 0f);
            if (Projectile.localAI[0] >= num4)
            {
                Projectile.localAI[1] = 1f;
                Projectile.Kill();
                return;
            }
            Player player = Main.player[Projectile.owner];
            float fromValue = Projectile.localAI[0] / Projectile.ai[1];
            float num6 = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] * 0.4f, num4, 0f, 1f);
            Projectile.direction = (Projectile.spriteDirection = (int)Projectile.ai[0]);
            int num7 = 3;
            if (Projectile.damage != 0 && Projectile.localAI[0] >= num5 + (float)num7)
            {
                Projectile.damage = 0;
            }
            if (Projectile.damage != 0)
            {
                int num8 = 80;
                bool flag = false;
                float num9 = Projectile.velocity.ToRotation();
                for (float num10 = -1f; num10 <= 1f; num10 += 0.5f)
                {
                    Vector2 position = Projectile.Center + (num9 + num10 * ((float)Math.PI / 4f) * 0.25f).ToRotationVector2() * num8 * 0.5f * Projectile.scale;
                    Vector2 position2 = Projectile.Center + (num9 + num10 * ((float)Math.PI / 4f) * 0.25f).ToRotationVector2() * num8 * Projectile.scale;
                    if (!Collision.SolidTiles(Projectile.Center, 0, 0) && Collision.CanHit(position, 0, 0, position2, 0, 0))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    Projectile.damage = 0;
                }
            }
            fromValue = Projectile.localAI[0] / Projectile.ai[1];
            Projectile.localAI[1] += 1f;
            num6 = Utils.Remap(Projectile.localAI[1], Projectile.ai[1] * 0.4f, num4, 0f, 1f);
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter) - Projectile.velocity + Projectile.velocity * num6 * num6 * num5;
            Projectile.rotation += Projectile.ai[0] * ((float)Math.PI * 2f) * (4f + Projectile.Opacity * 4f) / 90f;
            Projectile.scale = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] + 2f, num4, 1.12f, 1f) * Projectile.ai[2];
            float f = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
            Vector2 vector = Projectile.Center + f.ToRotationVector2() * 84f * Projectile.scale;
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Dust.NewDustPerfect(vector, DustID.Torch, null, 150, default(Color), 1.4f);
                dust.noLight = (dust.noLightEmittence = true);
            }
            for (int i = 0; (float)i < 3f * Projectile.Opacity; i++)
            {
                Vector2 vector2 = Projectile.velocity.SafeNormalize(Vector2.UnitX);
                int num11 = ((Main.rand.NextFloat() < Projectile.Opacity) ? DustID.Torch : DustID.FlameBurst);
                Dust dust2 = Dust.NewDustPerfect(vector, num11, Projectile.velocity * 0.2f + vector2 * 3f, 100, default(Color), 1.4f);
                dust2.noGravity = true;
                dust2.customData = Projectile.Opacity * 0.2f;
            }
            Projectile.ownerHitCheck = Projectile.localAI[0] <= 6f;
            if (Projectile.localAI[0] >= MathHelper.Lerp(num3, num4, 0.65f))
            {
                Projectile.damage = 0;
            }
            float fromValue2 = 1f - (1f - num6) * (1f - num6);
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.scale = Utils.Remap(fromValue2, 0f, 1f, 1.5f, 1f) * Projectile.ai[2];
            num6 = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] / 2f, num4, 0f, 1f);
            Projectile.Opacity = Utils.Remap(Projectile.localAI[0], 0f, Projectile.ai[1] * 0.5f, 0f, 1f) * Utils.Remap(Projectile.localAI[0], num4 - 12f, num4, 1f, 0f);
            if (Projectile.velocity.Length() > 8f)
            {
                Projectile.velocity *= 0.94f;
                new Vector2(32f, 32f);
                float num12 = Utils.Remap(fromValue, 0.7f, 1f, 110f, 110f);
                if (Projectile.localAI[1] == 0f)
                {
                    bool flag2 = false;
                    for (float num13 = -1f; num13 <= 1f; num13 += 0.5f)
                    {
                        Vector2 position3 = Projectile.Center + (Projectile.rotation + num13 * ((float)Math.PI / 4f) * 0.25f).ToRotationVector2() * num12 * 0.5f * Projectile.scale;
                        Vector2 position4 = Projectile.Center + (Projectile.rotation + num13 * ((float)Math.PI / 4f) * 0.25f).ToRotationVector2() * num12 * Projectile.scale;
                        if (Collision.CanHit(position3, 0, 0, position4, 0, 0))
                        {
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        Projectile.localAI[1] = 1f;
                    }
                }
                if (Projectile.localAI[1] == 1f && Projectile.velocity.Length() > 8f)
                {
                    Projectile.velocity *= 0.8f;
                }
                if (Projectile.localAI[1] == 1f)
                {
                    Projectile.velocity *= 0.88f;
                }
            }
            float num14 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.9f;
            Vector2 vector3 = Projectile.Center + num14.ToRotationVector2() * 85f * Projectile.scale;
            (num14 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
            Color value = Color.Red;
            Color value2 = Color.Orange;
            Lighting.AddLight(Projectile.Center + Projectile.rotation.ToRotationVector2() * 85f * Projectile.scale, value.ToVector3());
            for (int j = 0; j < 3; j++)
            {
                if (Main.rand.NextFloat() < Projectile.Opacity + 0.1f)
                {
                    Color.Lerp(Color.Lerp(Color.Lerp(value2, value, Utils.Remap(fromValue, 0f, 0.6f, 0f, 1f)), Color.White, Utils.Remap(fromValue, 0.6f, 0.8f, 0f, 0.5f)), Color.White, Main.rand.NextFloat() * 0.3f);
                    Dust dust3 = Dust.NewDustPerfect(vector3, DustID.FlameBurst, Projectile.velocity * 0.7f, 100, default(Color) * Projectile.Opacity, 0.8f * Projectile.Opacity);
                    dust3.scale *= 0.7f;
                    dust3.velocity += player.velocity * 0.1f;
                    dust3.position -= dust3.velocity * 6f;
                }
            }
            if (Projectile.damage == 0)
            {
                Projectile.localAI[0] += 3f;
                Projectile.velocity *= 0.76f;
            }
            if (Projectile.localAI[0] < 10f && (Projectile.localAI[1] == 1f || Projectile.damage == 0))
            {
                Projectile.localAI[0] += 1f;
                Projectile.velocity *= 0.85f;
                for (int k = 0; k < 4; k++)
                {
                    float num15 = Main.rand.NextFloatDirection();
                    float num16 = 1f - Math.Abs(num15);
                    num14 = Projectile.rotation + num15 * ((float)Math.PI / 2f) * 0.9f;
                    vector3 = Projectile.Center + num14.ToRotationVector2() * 85f * Projectile.scale;
                    Color.Lerp(Color.Lerp(Color.Lerp(value2, value, Utils.Remap(fromValue, 0f, 0.6f, 0f, 1f)), Color.White, Utils.Remap(fromValue, 0.6f, 0.8f, 0f, 0.5f)), Color.White, Main.rand.NextFloat() * 0.3f);
                    Dust dust4 = Dust.NewDustPerfect(vector3, DustID.FlameBurst, Projectile.velocity.RotatedBy(num15 * ((float)Math.PI / 4f)) * 0.2f * Main.rand.NextFloat(), 100, default(Color), 1.4f * num16);
                    dust4.velocity += player.velocity * 0.1f;
                    dust4.position -= dust4.velocity * Main.rand.NextFloat() * 3f;

                }
            }
        }

        public override void CutTiles()
        {
            Vector2 starting = (Projectile.rotation - MathHelper.PiOver4).ToRotationVector2() * 60f * Projectile.scale;
            Vector2 ending = (Projectile.rotation + MathHelper.PiOver4).ToRotationVector2() * 60f * Projectile.scale;
            float width = 60f * Projectile.scale;
            Utils.PlotTileLine(Projectile.Center + starting, Projectile.Center + ending, width, DelegateMethods.CutTiles);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.FlameWaders,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);

            hit.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.Excalibur,
                new ParticleOrchestraSettings { PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox) },
                Projectile.owner);

            info.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 vector = Projectile.Center - Main.screenPosition;
            Asset<Texture2D> asset = TextureAssets.Projectile[Projectile.type];
            Rectangle rectangle = asset.Frame(1, 4);
            Vector2 origin = rectangle.Size() / 2f;
            float scale = Projectile.scale;
            SpriteEffects spriteEffects = ((!(Projectile.ai[0] >= 0f)) ? SpriteEffects.FlipVertically : SpriteEffects.None);
            SpriteEffects effects = spriteEffects ^ SpriteEffects.FlipVertically;
            float num = Utils.Remap(Projectile.localAI[0], 0f, Projectile.ai[1] + 30f, 0f, 1f);
            float opacity = Projectile.Opacity;
            float num2 = 0.975f;
            float num3 = Lighting.GetColor(Projectile.Center.ToTileCoordinates()).ToVector3().Length() / (float)Math.Sqrt(3.0);
            num3 = 0.5f + num3 * 0.5f;
            num3 = Utils.Remap(num3, 0.2f, 1f, 0f, 1f);
            Color color = Color.Red;
            Main.spriteBatch.Draw(asset.Value, vector, rectangle, color * num3 * opacity, Projectile.rotation + Projectile.ai[0] * ((float)Math.PI / 4f) * 0.5f * -1f * (1f - num), origin, scale * 0.95f, spriteEffects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, rectangle, color * num3 * opacity, Projectile.rotation + Projectile.ai[0] * ((float)Math.PI / 4f) * 0.5f * 1f * (1f - num), origin, scale * 0.95f, effects, 0f);
            Color color2 = Color.OrangeRed;
            Color color3 = Color.Orange;
            Color color4 = Color.White * opacity * 0.5f;
            color4.A = (byte)((float)(int)color4.A * (1f - num3));
            Color color5 = color4 * num3 * 0.5f;
            color5.G = (byte)((float)(int)color5.G * num3);
            color5.B = (byte)((float)(int)color5.R * (0.25f + num3 * 0.75f));
            Main.spriteBatch.Draw(asset.Value, vector, rectangle, color5 * 0.15f, Projectile.rotation + Projectile.ai[0] * 0.01f, origin, scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, rectangle, color5 * 0.15f, Projectile.rotation + Projectile.ai[0] * -0.01f, origin, scale, effects, 0f);
            float num4 = 1f - num;
            float num5 = 0.25f;
            float num6 = 0.15f;
            float num7 = 0.05f;
            Main.spriteBatch.Draw(asset.Value, vector, rectangle, color3 * num3 * opacity * 0.3f, Projectile.rotation + Projectile.ai[0] * num5 * num4, origin, scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, rectangle, color3 * num3 * opacity * 0.3f, Projectile.rotation + (0f - Projectile.ai[0]) * num5 * num4, origin, scale, effects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, rectangle, color2 * num3 * opacity * 0.5f, Projectile.rotation + Projectile.ai[0] * num6 * num4, origin, scale * num2, spriteEffects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, asset.Frame(1, 4, 0, 3), Color.White * 0.6f * opacity, Projectile.rotation + Projectile.ai[0] * num7 * num4, origin, scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, asset.Frame(1, 4, 0, 3), Color.White * 0.5f * opacity, Projectile.rotation + Projectile.ai[0] * -0.05f, origin, scale * 0.8f, spriteEffects, 0f);
            Main.spriteBatch.Draw(asset.Value, vector, asset.Frame(1, 4, 0, 3), Color.White * 0.4f * opacity, Projectile.rotation + Projectile.ai[0] * -0.1f, origin, scale * 0.6f, spriteEffects, 0f);
            for (float num8 = -9f; num8 < 9f; num8 += 1f)
            {
                float num9 = Projectile.rotation + Projectile.ai[0] * num8 * ((float)Math.PI * -2f) * 0.025f;
                Vector2 drawpos = vector + num9.ToRotationVector2() * ((float)asset.Width() * 0.5f - 6f) * scale;
                float num10 = Math.Abs(num8) / 9f;
                DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawpos, new Color(255, 255, 255, 0) * opacity * num10, color3, num, 0f, 0.5f, 0.5f, 1f, num9, new Vector2(0f, Utils.Remap(num, 0f, 1f, 3f, 0f)) * scale, Vector2.One * scale);
            }
            for (float num11 = -1f; num11 <= 1f; num11 += 0.5f)
            {
                if (num11 != 0f)
                {
                    Vector2 drawpos2 = vector + (Projectile.rotation + num11 * (float)Math.PI * 0.75f * num).ToRotationVector2() * ((float)asset.Width() * 0.5f - 4f) * scale;
                    float num12 = Utils.Remap(Math.Abs(num11), 0f, 1f, 1f, 0.5f);
                    DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawpos2, new Color(255, 255, 255, 0) * opacity * 0.5f, color3, num, 0f, 0.5f, 0.5f, 0.75f, (float)Math.PI / 4f, new Vector2(Utils.Remap(num, 0f, 1f, 4f, 1f)) * scale * num12, Vector2.One * scale * num12);
                    DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawpos2, new Color(255, 255, 255, 0) * opacity * 0.5f, color3, num, 0f, 0.5f, 0.5f, 0.75f, 0f, new Vector2(2f, Utils.Remap(num, 0f, 1f, 4f, 1f)) * scale * num12, Vector2.One * scale * num12);
                }
            }
            Vector2 drawpos3 = vector + Projectile.rotation.ToRotationVector2() * ((float)asset.Width() * 0.5f - 4f) * scale;
            DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawpos3, new Color(255, 255, 255, 0) * opacity * 0.5f, color3, num, 0f, 0.5f, 0.5f, 1f, (float)Math.PI / 4f, new Vector2(Utils.Remap(num, 0f, 1f, 4f, 1f)) * scale, Vector2.One * scale * 1.5f);
            DrawPrettyStarSparkle(Projectile.Opacity, SpriteEffects.None, drawpos3, new Color(255, 255, 255, 0) * opacity * 0.5f, color3, num, 0f, 0.5f, 0.5f, 1f, 0f, new Vector2(2f, Utils.Remap(num, 0f, 1f, 4f, 1f)) * scale, Vector2.One * scale * 1.5f);

            return false;
        }

        private static void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawPos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
        {
            Texture2D sparkleTexture = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Color bigColor = shineColor * opacity * 0.5f;
            bigColor.A = 0;
            Vector2 origin = sparkleTexture.Size() / 2f;
            Color smallColor = drawColor * 0.5f;
            float lerpValue = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
            Vector2 scaleLeftRight = new Vector2(fatness.X * 0.5f, scale.X) * lerpValue;
            Vector2 scaleUpDown = new Vector2(fatness.Y * 0.5f, scale.Y) * lerpValue;
            bigColor *= lerpValue;
            smallColor *= lerpValue;
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, bigColor, MathHelper.PiOver2 + rotation, origin, scaleLeftRight, dir);
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, bigColor, 0f + rotation, origin, scaleUpDown, dir);
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, smallColor, MathHelper.PiOver2 + rotation, origin, scaleLeftRight * 0.6f, dir);
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, smallColor, 0f + rotation, origin, scaleUpDown * 0.6f, dir);
        }
    }
}
