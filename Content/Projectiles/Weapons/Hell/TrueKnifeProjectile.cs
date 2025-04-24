using Eternal.Content.Buffs.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Hell
{
    public class TrueKnifeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;

            Projectile.rotation += 15f;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            Projectile.Kill();
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            player.AddBuff(ModContent.BuffType<BloodyMurder>(), 20 * 60);
            target.AddBuff(ModContent.BuffType<FatalPenetration>(), 20 * 60);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Player player = Main.player[Projectile.owner];

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
            }

            player.AddBuff(ModContent.BuffType<BloodyMurder>(), 20 * 60);
            target.AddBuff(ModContent.BuffType<FatalPenetration>(), 20 * 60);
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/Projectiles/Weapons/Hell/TrueKnifeProjectile_Shadow").Value;

            Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
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
