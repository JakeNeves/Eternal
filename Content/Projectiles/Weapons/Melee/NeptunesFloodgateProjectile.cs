using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class NeptunesFloodgateProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 96;
            Projectile.height = 96;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 100;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (!Main.dedServ)
                Lighting.AddLight(Projectile.position, 0.55f, 1.56f, 1.10f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 47 && targetHitbox.Height > 35)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 47, -targetHitbox.Height / 35);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.UltraBrightTorch, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);


            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.BlueTorch, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);

            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BOTAHit")
                {
                    Volume = 0.8f,
                    Pitch = Main.rand.NextFloat(-0.75f, 0.25f),
                    MaxInstances = 0,
                }, Projectile.position);
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                for (int i = 0; i < Main.rand.Next(4, 12); i++)
                {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center + new Vector2(Main.rand.NextFloat(-2.75f, 2.75f), Main.rand.NextFloat(-0.75f, 0.75f)), Projectile.velocity, ModContent.ProjectileType<FloodgateDroplet>(), Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                }
            }

            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(8f, 8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(8f, -8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }

            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(8f, 8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-8f, 8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(-8f, -8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(8f, -8f), ModContent.ProjectileType<NeptunesFloodgateProjectile2>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/Projectiles/Weapons/Melee/NeptunesFloodgateProjectile_Shadow").Value;

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
