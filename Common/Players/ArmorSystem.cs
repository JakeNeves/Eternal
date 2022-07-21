using Eternal.Content.Projectiles.Armor;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class ArmorSystem : ModPlayer
    {
        public static bool StarbornArmor = false;
        public static bool ArkaniumArmor = false;
        public static bool UltimusArmor = false;
        public static bool SubzeroArmor = false;
        public static bool IgneousArmor = false;
        public static bool DuneshockArmor = false;
        public static bool CosmicKeeperArmor = false;
        public static bool IesniumArmor = false;

        public override void ResetEffects()
        {
            StarbornArmor = false;
            ArkaniumArmor = false;
            UltimusArmor = false;
            SubzeroArmor = false;
            IgneousArmor = false;
            DuneshockArmor = false;
            CosmicKeeperArmor = false;
            IesniumArmor = false;
        }

        public override void PreUpdate()
        {
            if (StarbornArmor)
            {
                if (Player.statLifeMax < Player.statLifeMax2 / 2)
                {
                    Player.GetDamage(DamageClass.Generic) += 0.15f;
                }
            }
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            var entitySource = Player.GetSource_None();

            if (ArkaniumArmor)
            {
                for (int i = 0; i < Main.rand.Next(8, 16); i++)
                    Projectile.NewProjectile(entitySource, Player.position.X + Main.rand.Next(-200, 200), Player.position.Y - 600, Main.rand.Next(-4, 4), 16, ModContent.ProjectileType<ArkaniumSword>(), (int)damage * 2, 0, Player.whoAmI);
            }
        }
    }
}
