using System;
using System.Collections.Generic;
using Eternal.Buffs.Minions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Projectiles.Minions
{
    public class CryonicEnergy : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryonic Energy");
        }

        public override void SetDefaults()
        {
            projectile.width = 66;
            projectile.height = 66;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0.25f;
            projectile.timeLeft = 18000;
            projectile.alpha = 128;
            projectile.light = 0.25f;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            aiType = 388;
            projectile.aiStyle = 66;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.position, 60f, 46f, 54f);
            projectile.rotation += projectile.velocity.X * 0.01f;
            bool projType = projectile.type == ProjectileType<CryonicEnergy>();
            Player player = Main.player[projectile.owner];
            EternalPlayer modPlayer = player.GetModPlayer<EternalPlayer>();
            player.AddBuff(mod.BuffType("CryonicEnergyBuff"), 3600);
            if (projType)
            {
                if (player.dead)
                {
                    modPlayer.cEnergy = false;
                }
                if (modPlayer.cEnergy)
                {
                    projectile.timeLeft = 2;
                }
            }
        }

    }
}
