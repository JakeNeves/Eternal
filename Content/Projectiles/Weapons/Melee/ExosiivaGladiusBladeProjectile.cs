using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class ExosiivaGladiusBladeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 76;
            Projectile.height = 76;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int k = 0; k < Main.rand.Next(3, 6); k++)
            {
                Projectile.NewProjectile(entitySource, (int)Projectile.Center.X + Main.rand.Next(-200, 200), (int)Projectile.Center.Y + Main.rand.Next(-200, 200), (float)Projectile.velocity.X, (float)Projectile.velocity.Y, ModContent.ProjectileType<ExosiivaGladiusBladeProjectile2>(), Projectile.damage / 2, -1f);
            }

            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Exosiiva>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, Color.White, Main.rand.NextFloat(0.5f, 1f));
            }
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(Projectile.Center, 0.36f, 2.03f, 2.09f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            if (Projectile.alpha > 0)
                Projectile.alpha -= 5;

            for (int k = 0; k < Main.rand.Next(2, 4); k++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<Exosiiva>());
                dust.noGravity = true;
                dust.velocity = new Vector2(Main.rand.NextFloat(0.5f, 1f), Main.rand.NextFloat(0.5f, 1f));
                dust.scale = Main.rand.NextFloat(0.25f, 1.5f);
            }

            if (Projectile.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Exosiiva>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, Color.White, Main.rand.NextFloat(0.5f, 1f));
                }

                Projectile.ai[0] = 1f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Exosiiva>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
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
