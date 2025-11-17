using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class ExosiivaGladiusBladeProjectile2 : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Weapons/Melee/ExosiivaGladiusBladeProjectile";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 88;
            Projectile.height = 88;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 200;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(Projectile.Center, 0.36f, 2.03f, 2.09f);

            if (Projectile.alpha > 0)
                Projectile.alpha -= 5;

            for (int k = 0; k < Main.rand.Next(2, 4); k++)
            {
                if (Main.rand.NextBool(2))
                {
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Exosiiva>());
                    dust.noGravity = true;
                    dust.velocity = new Vector2(Main.rand.NextFloat(0.5f, 1f), Main.rand.NextFloat(0.25f, 1.5f));
                }
            }

            if (Projectile.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.rotation = Main.rand.NextFloat(15f, 45f);

                if (Main.rand.NextBool(2))
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Exosiiva>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, Color.White, Main.rand.NextFloat(0.5f, 1f));

                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ExosiivaGladiusBlade")
                    {
                        Volume = 0.25f,
                        PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                        MaxInstances = 0,
                    }, Projectile.position);
                }

                Projectile.ai[0] = 1f;
            }

            if (Projectile.timeLeft < 190)
            {
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

                float maxDetectRadius = 250f;
                float projSpeed = 24f;

                NPC closestNPC = FindClosestNPC(maxDetectRadius);
                if (closestNPC == null)
                    return;

                Projectile.velocity = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            }

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Exosiiva>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        }

        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                if (target.CanBeChasedBy())
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
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
