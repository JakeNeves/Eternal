using Eternal.Content.Dusts;
using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Magic
{
    public class PustuleWandProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Puss>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
        }

        public override void OnKill(int timeLeft)
        {
            if (!Main.dedServ)
                SoundEngine.PlaySound(SoundID.NPCDeath22, Projectile.position);

            for (int i = 0; i < Main.rand.Next(3, 6); i++)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.position, new Vector2(Projectile.velocity.X + Main.rand.NextFloat(-4f, 4f), -4f), ModContent.ProjectileType<BallofPuss>(), Projectile.damage, 0f);

                Main.projectile[proj].DamageType = DamageClass.Magic;
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            int size = 30;
            hitbox.X -= size;
            hitbox.Y -= size;
            hitbox.Width += size * 2;
            hitbox.Height += size * 2;
        }
    }
}
