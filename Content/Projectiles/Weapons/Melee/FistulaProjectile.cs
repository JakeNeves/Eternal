using Eternal.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Weapons.Melee
{
    public class FistulaProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 200f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 10f;
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(36))
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Puss>(), Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f);
                }
            }

            if (Main.rand.NextBool(12))
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.NPCHit13, Projectile.position);

                for (int i = 0; i < 1 + Main.rand.Next(4); i++)
                {
                    if (!Main.dedServ)
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), ModContent.ProjectileType<BallofPuss>(), Projectile.damage, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                }
            }
        }
    }
}
