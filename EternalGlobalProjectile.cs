using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
    public class EternalGlobalProjectile : GlobalProjectile
    {
        public static bool cometGauntlet = false;
        public static bool emperorsGift = false;

        public static bool piercingBuff = false;

        public override void AI(Projectile projectile)
        {
            if (projectile.melee && cometGauntlet)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust dust = Dust.NewDustDirect(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Shadowflame, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                    dust.fadeIn = 0.4f;
                }
            }

            if (emperorsGift)
            {
                if (projectile.melee || projectile.ranged || projectile.magic || projectile.minion)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, ModContent.DustType<EmperorFire>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
                    }
                }
            }

        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (piercingBuff)
            {
                target.AddBuff(ModContent.BuffType<Buffs.RedFracture>(), 1024);
            }
        }

        public static Projectile NewProjectileDirectSafe(Vector2 pos, Vector2 vel, int type, int damage, float knockback, int owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int pro = Projectile.NewProjectile(pos, vel, type, damage, knockback, owner, ai0, ai1);
            return (pro < 1000) ? Main.projectile[pro] : null;
        }

    }

}
