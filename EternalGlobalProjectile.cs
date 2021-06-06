using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal
{
    public class EternalGlobalProjectile : GlobalProjectile
    {
        public static bool cometGauntlet = false;

        public override void AI(Projectile projectile)
        {
            if (projectile.melee == true && cometGauntlet == true)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<Starmetal>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                }
            }

        }

        public static Projectile NewProjectileDirectSafe(Vector2 pos, Vector2 vel, int type, int damage, float knockback, int owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int pro = Projectile.NewProjectile(pos, vel, type, damage, knockback, owner, ai0, ai1);
            return (pro < 1000) ? Main.projectile[pro] : null;
        }

    }

}
