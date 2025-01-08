using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class BOTAProjectile : ModProjectile
    {
        int projTimer = 15;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 450;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            var entitySource = Projectile.GetSource_FromAI();

            if (projTimer <= 0)
            {
                projTimer = 15;
                Projectile.NewProjectile(entitySource, Projectile.Center, Projectile.velocity / 2, ModContent.ProjectileType<BOTAProjectileTrail>(), Projectile.damage / 2, Projectile.knockBack / 0.5f);
            }
            else
                projTimer--;

            if (!Main.dedServ)
                Lighting.AddLight(Projectile.position, 0.24f, 0.32f, 0.32f);

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 6; i++)
            {
                if (!Main.dedServ)
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(Main.rand.Next(-8, 8), Main.rand.Next(-8, 8)), ModContent.ProjectileType<BOTAProjectileTrail>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }

            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BOTAHit")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(1f, 1.25f),
                    MaxInstances = 0,
                }, Projectile.position);
            }

            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_Death();

            for (int i = 0; i < 6; i++)
            {
                if (!Main.dedServ)
                    Projectile.NewProjectile(entitySource, Projectile.Center, new Vector2(Main.rand.Next(-8, 8), Main.rand.Next(-8, 8)), ModContent.ProjectileType<BOTAProjectileAOE>(), Projectile.damage / 2, 0.0f, Main.myPlayer, 0.0f, 0.0f);
            }

            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BOTAHit")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(1f, 1.25f),
                    MaxInstances = 0,
                }, Projectile.position);
            }
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
