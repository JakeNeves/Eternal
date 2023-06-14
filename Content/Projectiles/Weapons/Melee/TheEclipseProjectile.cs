using Eternal.Common.GlobalProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class TheEclipseProjectile : ModProjectile
    {
        public bool yoyoSpawn = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Eclipse");

            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 360f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 15f;

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 16;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
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
        }

        public override void AI()
        {
            if (!yoyoSpawn && Projectile.owner == Main.myPlayer)
            {
                int maxYoyos = 4;
                for (int i = 0; i < maxYoyos; i++)
                {
                    float radians = (360f / (float)maxYoyos) * i * (float)(Math.PI / 180);
                    Projectile yoyo = WeaponGlobalProjectile.NewProjectileDirectSafe(Projectile.Center, Vector2.Zero, ModContent.ProjectileType<TheEclipseOrbit>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 5, radians);
                    yoyo.localAI[0] = Projectile.whoAmI;
                }

                yoyoSpawn = true;
            }


            if (Main.player[Projectile.owner].HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.TheEclipse>())
            {
                Projectile.damage = Main.player[Projectile.owner].GetWeaponDamage(Main.player[Projectile.owner].HeldItem);
                Projectile.knockBack = Main.player[Projectile.owner].GetWeaponKnockback(Main.player[Projectile.owner].HeldItem, Main.player[Projectile.owner].HeldItem.knockBack);
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return (Projectile.Distance(targetHitbox.Center()) <= 70);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/Projectiles/Weapons/Melee/TheEclipseProjectile_Shadow").Value;

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
