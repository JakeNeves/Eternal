using System;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Projectiles.Minions
{
    public abstract class Minion : ModProjectile
    {
        public override void AI()
        {
            CheckActive();
            Behaviour();
        }

        public abstract void CheckActive();

        public abstract void Behaviour();

    }
}
