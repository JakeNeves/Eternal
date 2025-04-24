using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class PrystaltineLaser : ModProjectile
    {
        private const float PiBeamDivisor = MathHelper.Pi / PrystaltineHoldout.NumBeams;

        private const float MaxDamageMultiplier = 1.5f;

        private const float MaxBeamScale = 1.8f;

        private const float MaxBeamSpread = 2f;

        private const float MaxBeamLength = 2400f;

        private const float BeamTileCollisionWidth = 1f;

        private const float BeamHitboxCollisionWidth = 22f;

        private const int NumSamplePoints = 3;

        private const float BeamLengthChangeFactor = 0.75f;

        private const float VisualEffectThreshold = 0.1f;

        private const float OuterBeamOpacityMultiplier = 0.75f;
        private const float InnerBeamOpacityMultiplier = 0.1f;

        private const float BeamLightBrightness = 0.75f;

        private const float BeamColorHue = 1.24f;
        private const float BeamHueVariance = 1.16f;
        private const float BeamColorSaturation = 1.15f;
        private const float BeamColorLightness = 0.3f;

        private float BeamID
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private float HostPrismIndex
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        private float BeamLength
        {
            get => Projectile.localAI[1];
            set => Projectile.localAI[1] = value;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.tileCollide = false;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        public override void SendExtraAI(BinaryWriter writer) => writer.Write(BeamLength);
        public override void ReceiveExtraAI(BinaryReader reader) => BeamLength = reader.ReadSingle();

        public override void AI()
        {
            Projectile hostPrism = Main.projectile[(int)HostPrismIndex];
            if (Projectile.type != ModContent.ProjectileType<PrystaltineLaser>() || !hostPrism.active || hostPrism.type != ModContent.ProjectileType<PrystaltineHoldout>())
            {
                Projectile.Kill();
                return;
            }

            Vector2 hostPrismDir = Vector2.Normalize(hostPrism.velocity);
            float chargeRatio = MathHelper.Clamp(hostPrism.ai[0] / PrystaltineHoldout.MaxCharge, 0f, 1f);

            Projectile.damage = (int)(hostPrism.damage * GetDamageMultiplier(chargeRatio));

            Projectile.friendly = hostPrism.ai[0] > PrystaltineHoldout.DamageStart;

            float beamIdOffset = BeamID - PrystaltineHoldout.NumBeams / 2f + 0.5f;
            float beamSpread;
            float spinRate;
            float beamStartSidewaysOffset;
            float beamStartForwardsOffset;

            if (chargeRatio < 1f)
            {
                Projectile.scale = MathHelper.Lerp(0f, MaxBeamScale, chargeRatio);
                beamSpread = MathHelper.Lerp(MaxBeamSpread, 0f, chargeRatio);
                beamStartSidewaysOffset = MathHelper.Lerp(20f, 6f, chargeRatio);
                beamStartForwardsOffset = MathHelper.Lerp(-21f, -17f, chargeRatio);

                if (chargeRatio <= 0.66f)
                {
                    float phaseRatio = chargeRatio * 1.5f;
                    Projectile.Opacity = MathHelper.Lerp(0f, 0.4f, phaseRatio);
                    spinRate = MathHelper.Lerp(20f, 16f, phaseRatio);
                }

                else
                {
                    float phaseRatio = (chargeRatio - 0.66f) * 3f;
                    Projectile.Opacity = MathHelper.Lerp(0.4f, 1f, phaseRatio);
                    spinRate = MathHelper.Lerp(16f, 6f, phaseRatio);
                }
            }

            else
            {
                Projectile.scale = MaxBeamScale;
                Projectile.Opacity = 1f;
                beamSpread = 0f;
                spinRate = 6f;
                beamStartSidewaysOffset = 6f;
                beamStartForwardsOffset = -17f;
            }

            float deviationAngle = (hostPrism.ai[0] + beamIdOffset * spinRate) / (spinRate * PrystaltineHoldout.NumBeams) * MathHelper.TwoPi;

            Vector2 unitRot = Vector2.UnitY.RotatedBy(deviationAngle);
            Vector2 yVec = new Vector2(4f, beamStartSidewaysOffset);
            float hostPrismAngle = hostPrism.velocity.ToRotation();
            Vector2 beamSpanVector = (unitRot * yVec).RotatedBy(hostPrismAngle);
            float sinusoidYOffset = unitRot.Y * PiBeamDivisor * beamSpread;

            Projectile.Center = hostPrism.Center;
            Projectile.position += hostPrismDir * 16f + new Vector2(0f, -hostPrism.gfxOffY);
            Projectile.position += hostPrismDir * beamStartForwardsOffset;
            Projectile.position += beamSpanVector;

            Projectile.velocity = hostPrismDir.RotatedBy(sinusoidYOffset);
            if (Projectile.velocity.HasNaNs() || Projectile.velocity == Vector2.Zero)
            {
                Projectile.velocity = -Vector2.UnitY;
            }
            Projectile.rotation = Projectile.velocity.ToRotation();

            float hitscanBeamLength = PerformBeamHitscan(hostPrism, chargeRatio >= 1f);
            BeamLength = MathHelper.Lerp(BeamLength, hitscanBeamLength, BeamLengthChangeFactor);

            Vector2 beamDims = new Vector2(Projectile.velocity.Length() * BeamLength, Projectile.width * Projectile.scale);

            Color beamColor = GetOuterBeamColor();
            if (chargeRatio >= VisualEffectThreshold)
            {
                ProduceBeamDust(beamColor);

                if (Main.netMode != NetmodeID.Server)
                {
                    ProduceWaterRipples(beamDims);
                }
            }

            DelegateMethods.v3_1 = beamColor.ToVector3() * BeamLightBrightness * chargeRatio;
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * BeamLength, beamDims.Y, new Utils.TileActionAttempt(DelegateMethods.CastLight));
        }

        private float GetDamageMultiplier(float chargeRatio)
        {
            float f = chargeRatio * chargeRatio * chargeRatio;
            return MathHelper.Lerp(1f, MaxDamageMultiplier, f);
        }

        private float PerformBeamHitscan(Projectile prism, bool fullCharge)
        {
            Vector2 samplingPoint = Projectile.Center;
            if (fullCharge)
            {
                samplingPoint = prism.Center;
            }

            Player player = Main.player[Projectile.owner];
            if (!Collision.CanHitLine(player.Center, 0, 0, prism.Center, 0, 0))
            {
                samplingPoint = player.Center;
            }

            float[] laserScanResults = new float[NumSamplePoints];
            Collision.LaserScan(samplingPoint, Projectile.velocity, 0 * Projectile.scale, MaxBeamLength, laserScanResults);
            float averageLengthSample = 0f;
            for (int i = 0; i < laserScanResults.Length; ++i)
            {
                averageLengthSample += laserScanResults[i];
            }
            averageLengthSample /= NumSamplePoints;

            return averageLengthSample;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }

            float _ = float.NaN;
            Vector2 beamEndPos = Projectile.Center + Projectile.velocity * BeamLength;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, beamEndPos, BeamHitboxCollisionWidth * Projectile.scale, ref _);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 centerFloored = Projectile.Center.Floor() + Projectile.velocity * Projectile.scale * 10.5f;
            Vector2 drawScale = new Vector2(Projectile.scale);

            float visualBeamLength = BeamLength - 14.5f * Projectile.scale * Projectile.scale;

            DelegateMethods.f_1 = 1f;
            Vector2 startPosition = centerFloored - Main.screenPosition;
            Vector2 endPosition = startPosition + Projectile.velocity * visualBeamLength;

            DrawBeam(Main.spriteBatch, texture, startPosition, endPosition, drawScale, GetOuterBeamColor() * OuterBeamOpacityMultiplier * Projectile.Opacity);

            drawScale *= 0.5f;
            DrawBeam(Main.spriteBatch, texture, startPosition, endPosition, drawScale, GetInnerBeamColor() * InnerBeamOpacityMultiplier * Projectile.Opacity);

            return false;
        }

        private void DrawBeam(SpriteBatch spriteBatch, Texture2D texture, Vector2 startPosition, Vector2 endPosition, Vector2 drawScale, Color beamColor)
        {
            Utils.LaserLineFraming lineFraming = new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw);

            DelegateMethods.c_1 = beamColor;
            Utils.DrawLaser(spriteBatch, texture, startPosition, endPosition, drawScale, lineFraming);
        }

        private Color GetOuterBeamColor()
        {
            float hue = (BeamID / PrystaltineHoldout.NumBeams) % BeamHueVariance + BeamColorHue;

            Color c = Main.hslToRgb(hue, BeamColorSaturation, BeamColorLightness);

            c.A = 64;
            return c;
        }

        private Color GetInnerBeamColor() => Color.White;

        private void ProduceBeamDust(Color beamColor)
        {
            Vector2 endPosition = Projectile.Center + Projectile.velocity * (BeamLength - 14.5f * Projectile.scale);

            float angle = Projectile.rotation + (Main.rand.NextBool() ? 1f : -1f) * MathHelper.PiOver2;
            float startDistance = Main.rand.NextFloat(1f, 1.8f);
            float scale = Main.rand.NextFloat(0.7f, 1.1f);
            Vector2 velocity = angle.ToRotationVector2() * startDistance;
            Dust dust = Dust.NewDustDirect(endPosition, 0, 0, DustID.UltraBrightTorch, velocity.X, velocity.Y, 0, beamColor, scale);
            dust.color = beamColor;
            dust.noGravity = true;

            if (Projectile.scale > 1f)
            {
                dust.velocity *= Projectile.scale;
                dust.scale *= Projectile.scale;
            }
        }

        private void ProduceWaterRipples(Vector2 beamDims)
        {
            WaterShaderData shaderData = (WaterShaderData)Filters.Scene["WaterDistortion"].GetShader();

            float waveSine = 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 20f);
            Vector2 ripplePos = Projectile.position + new Vector2(beamDims.X * 0.5f, 0f).RotatedBy(Projectile.rotation);

            Color waveData = new Color(0.5f, 0.1f * Math.Sign(waveSine) + 0.5f, 0f, 1f) * Math.Abs(waveSine);
            shaderData.QueueRipple(ripplePos, waveData, beamDims, RippleShape.Square, Projectile.rotation);
        }


        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Utils.TileActionAttempt cut = new Utils.TileActionAttempt(DelegateMethods.CutTiles);
            Vector2 beamStartPos = Projectile.Center;
            Vector2 beamEndPos = beamStartPos + Projectile.velocity * BeamLength;

            Utils.PlotTileLine(beamStartPos, beamEndPos, Projectile.width * Projectile.scale, cut);
        }
    }
}
