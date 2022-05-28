using Eternal.Common.Players;
using Eternal.Content.Projectiles.Accessories;
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
            var entitySource = Entity.GetSource_None();

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
