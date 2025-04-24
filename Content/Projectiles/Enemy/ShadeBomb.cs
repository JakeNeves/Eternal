using Microsoft.Xna.Framework;
using Eternal.Content.Dusts;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace Eternal.Content.Projectiles.Enemy
{
    public class ShadeBomb : ModProjectile
    {
        ref float BombTimer => ref Projectile.ai[1];

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 100;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            var entitySource = Projectile.GetSource_FromAI();

            BombTimer++;

            if (BombTimer >= 15f && !Main.dedServ)
            {
                if (Projectile.velocity.X != 0)
                {
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, 4f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, -4f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
                }

                if (Projectile.velocity.Y != 0)
                {
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(4f, 0f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-4f, 0f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
                }

                BombTimer = 0;
            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Shade>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Shade>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }

            if (!Main.dedServ)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, 4f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, -4f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(4f, 0f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-4f, 0f), ModContent.ProjectileType<ShadeBomb2>(), Projectile.damage / 2, 0);
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

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            int height = texture.Height / 5;
            int y = height * Projectile.frame;
            Rectangle rect = new(0, y, texture.Width, height);
            Vector2 drawOrigin = new(texture.Width / 2, Projectile.height / 2);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, new Rectangle?(rect), color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, new Rectangle?(rect), Projectile.GetAlpha(lightColor), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);

            return true;
        }
    }
}
