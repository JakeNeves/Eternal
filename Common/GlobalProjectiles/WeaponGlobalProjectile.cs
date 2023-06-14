using Eternal.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalProjectiles
{
    public class WeaponGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
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

            if (ArmorSystem.StarbornArmor)
            {
                if (Player.statLifeMax < Player.statLifeMax2 / 2)
                {
                    Player.HealEffect(Main.rand.Next(20, 40), false);
                }
            }
        }

        public static Projectile NewProjectileDirectSafe(Vector2 pos, Vector2 vel, int type, int damage, float knockback, int owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            var entitySource = Projectile.GetSource_None();
            int pro = Projectile.NewProjectile(entitySource, pos, vel, type, damage, knockback, owner, ai0, ai1);
            return (pro < 1000) ? Main.projectile[pro] : null;
        }
    }
}
