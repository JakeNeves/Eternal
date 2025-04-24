using Eternal.Common.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class NaquadicaProjectile : ModProjectile
    {
        int projTimer = 30;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 240f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 16f;

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            var entitySource = Projectile.GetSource_FromAI();

            if (projTimer <= 0)
            {
                projTimer = 30;

                if (!Main.dedServ)
                {
                    SoundEngine.PlaySound(SoundID.Item130, Projectile.position);
                }

                Projectile.NewProjectile(entitySource, Projectile.Center, Projectile.velocity / 2, ModContent.ProjectileType<NaquadicaProjectileAOE>(), Projectile.damage / 2, Projectile.knockBack / 0.5f);
            }
            else
                projTimer--;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/Projectiles/Weapons/Melee/NaquadicaProjectile_Shadow").Value;

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
