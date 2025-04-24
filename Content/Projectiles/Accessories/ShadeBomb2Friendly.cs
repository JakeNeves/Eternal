using Microsoft.Xna.Framework;
using Eternal.Content.Dusts;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal.Content.Projectiles.Accessories
{
    public class ShadeBomb2Friendly : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Enemy/ShadeBomb";

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 90;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            for (int k = 0; k < 5; k++)
            {
                int dust = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Shade>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);

            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Shade>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
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
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/Projectiles/Enemy/ShadeBomb_Glow").Value;
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
