﻿using Microsoft.Xna.Framework;
using Eternal.Content.Dusts;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Content.Projectiles.Accessories;
using Terraria.ID;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class DarkArtsProjectile : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Enemy/ShadeBomb";

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Shade>(), Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);
        }

        public override void OnKill(int timeLeft)
        {
            var entitySource = Projectile.GetSource_Death();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(12f, 0f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, 12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(-12f, 0f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
                Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(0f, -12f), ModContent.ProjectileType<ShadeBombFriendly>(), Projectile.damage / 2, 0);
            }
        }

        /*
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
        */

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
