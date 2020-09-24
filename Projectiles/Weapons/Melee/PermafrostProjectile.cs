using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Eternal.Projectiles.Weapons.Melee
{
    class PermafrostProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 500f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 19f;
        }

        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 20;
            projectile.height = 20;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 120);
            target.AddBuff(BuffID.Frostburn, 120);
            target.AddBuff(BuffID.Frozen, 120);
        } 

        public override void PostAI()
        {
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 16);
                dust.noGravity = true;
                dust.scale = 1.6f;
            }
        }
    }
}
