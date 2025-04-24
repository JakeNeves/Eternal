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
    public class RiftsDivinityProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 86;
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

            for (int k = 0; k < Main.rand.Next(2, 4); k++)
            {
                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(SoundID.Item89, Projectile.position);

                    Projectile.NewProjectile(entitySource, (int)Projectile.Center.X + Main.rand.Next(-50, 50), (int)Projectile.Center.Y + Main.rand.Next(-50, 50), (float)Projectile.velocity.X, (float)Projectile.velocity.Y, ModContent.ProjectileType<NaquadicaProjectileAOE>(), Projectile.damage / 2, -1f);
                }
            }
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(Projectile.Center, 0.36f, 0f, 2.09f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            if (Projectile.alpha > 0)
                Projectile.alpha -= 5;
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
