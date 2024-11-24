using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class ShiftstormPortalProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        int timer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 92;
            Projectile.height = 92;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 600;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.PinkTorch);
                dust.noGravity = true;
                dust.velocity = Vector2.Normalize(position - Projectile.Center) * 2;
                dust.noLight = false;
                dust.fadeIn = 1f;
                dust.scale = Main.rand.NextFloat(0.25f, 1f);
            }
        }

        public override void AI()
        {
            var entitySource = Projectile.GetSource_FromAI();

            if (!Main.dedServ)
                Lighting.AddLight(Projectile.Center, 0.75f, 0f, 0.75f);

            Projectile.rotation += 0.15f;
            timer++;

            if (timer == 30)
            {
                for (int i = 0; i < Main.rand.Next(1, 6); i++)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, Projectile.Center.X + Main.rand.Next(-40, 40), Projectile.Center.Y, 0, 10, ModContent.ProjectileType<ShiftstormProjectile>(), Projectile.damage, 0);
                }
                timer = 0;
            }

            if (Projectile.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 30; i++)
                {
                    Vector2 position = Projectile.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 50 * i)) * 30;
                    Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.PinkTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - Projectile.Center) * 2;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                    dust.scale = Main.rand.NextFloat(0.25f, 1f);
                }

                Projectile.ai[0] = 1f;
            }
        }
    }
}
