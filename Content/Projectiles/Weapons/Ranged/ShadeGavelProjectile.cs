using Eternal.Content.Dusts;
using Eternal.Content.Projectiles.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Ranged
{
    public class ShadeGavelProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.rotation += 0.5f;

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Shade>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            Projectile.Kill();
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Shade>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_FromThis();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (Main.rand.NextBool(2))
                {
                    if (Main.rand.NextBool(4))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(12f, -12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                    if (Main.rand.NextBool(8))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-12f, 12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                    if (Main.rand.NextBool(12))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-12f, 12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                    if (Main.rand.NextBool(16))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(12f, -12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);

                }
                else
                {
                    if (Main.rand.NextBool(4))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(12f, 0f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                    if (Main.rand.NextBool(8))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, 12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                    if (Main.rand.NextBool(12))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                    if (Main.rand.NextBool(16))
                        Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, -12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                }
            }
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
