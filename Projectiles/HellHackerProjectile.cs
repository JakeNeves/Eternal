using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Projectiles
{
    class HellHackerProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Hacker");
        }

        public override void SetDefaults()
        {
            projectile.width = 86;
            projectile.height = 78;
            projectile.aiStyle = 3;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.magic = false;
            projectile.penetrate = 25;
            projectile.timeLeft = 300;
            projectile.light = 1.0f;
            projectile.extraUpdates = 1;
        }
    }
}
