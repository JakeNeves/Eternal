﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Boss
{
    public class CosmicEmperorKnife : ModProjectile
    {
        bool justSpawned = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cosmic Emperor's Knife");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 28;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.timeLeft = 0;
            Projectile.Kill();
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.position, 1.27f, 0.22f, 0.76f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);

            if (!justSpawned)
            {
                SoundEngine.PlaySound(SoundID.Item71, Projectile.position);
                justSpawned = true;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, Projectile.position);
            for (int i = 0; i < 50; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DemonTorch, 0f, 0f, 100, default(Color), 2f);
                Main.dust[dustIndex].velocity *= 1.4f;
            }

            Vector2 usePos = Projectile.position;
            Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2();
        }
    }
}
