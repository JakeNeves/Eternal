using Eternal.Common.Players;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalProjectiles
{
    public class WeaponGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player Player = Main.player[Main.myPlayer];

            if (ArmorSystem.IesniumArmor)
            {
                if (Player.statLifeMax < Player.statLifeMax2 / 2)
                {
                    Player.HealEffect(Main.rand.Next(10, 20), false);
                }
            }
        }
    }
}
