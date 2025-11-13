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
    public class ApparitionalDiskProjectile : ModProjectile
    {
        Vector2 CircleDirc = new Vector2(0.0f, 15f);

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;

            var entitySource = Projectile.GetSource_FromAI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_FromAI();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < 12; i++)
                {
                    CircleDirc = Utils.RotatedBy(CircleDirc, 0.30f, new Vector2());
                    int index5 = Projectile.NewProjectile(entitySource, Projectile.Center, CircleDirc, ModContent.ProjectileType<ApparitionalDiskPierce>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    int index6 = Projectile.NewProjectile(entitySource, Projectile.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<ApparitionalDiskPierce>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index5].timeLeft = 30;
                    Main.projectile[index6].timeLeft = 30;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            var entitySource = Projectile.GetSource_FromAI();

            if (!Main.dedServ)
            {
                for (int i = 0; i < 6; i++)
                {
                    CircleDirc = Utils.RotatedBy(CircleDirc, 0.15, new Vector2());
                    int index5 = Projectile.NewProjectile(entitySource, Projectile.Center, CircleDirc, ModContent.ProjectileType<ApparitionalDiskPierce>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    int index6 = Projectile.NewProjectile(entitySource, Projectile.Center, Utils.RotatedBy(CircleDirc, Math.PI, new Vector2()), ModContent.ProjectileType<ApparitionalDiskPierce>(), Projectile.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                    Main.projectile[index5].timeLeft = 30;
                    Main.projectile[index6].timeLeft = 30;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/Projectiles/Weapons/Melee/ApparitionalDiskProjectile_Shadow").Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }

            return true;
        }

        public override void PostDraw(Color lightColor)
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
        }
    }
}
