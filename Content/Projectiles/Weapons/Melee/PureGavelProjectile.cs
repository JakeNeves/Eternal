using Eternal.Content.Buffs.Weapons;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class PureGavelProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 3;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.extraUpdates = 5;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.1f;

            var entitySource = Projectile.GetSource_FromAI();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var entitySource = Projectile.GetSource_FromThis();

            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BOTAHit")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(1f, 1.5f),
                    MaxInstances = 0,
                }, Projectile.position);
            }

            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.DemonTorch, Projectile.oldVelocity.X * 1f, Projectile.oldVelocity.Y * 1f);

            for (int k = 0; k < Main.rand.Next(3, 6); k++)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int proj = Projectile.NewProjectile(entitySource, (int)Projectile.Center.X + Main.rand.Next(-100, 100), (int)Projectile.Center.Y + Main.rand.Next(-100, 100), 0, 0, ModContent.ProjectileType<CombatGavelProjectile2>(), Projectile.damage / 2, -1f);

                    Main.projectile[proj].DamageType = DamageClass.Melee;
                }
            }

            if (Main.rand.NextBool(12))
                target.AddBuff(ModContent.BuffType<ImmenseArmorFracture>(), 20 * 60);
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
