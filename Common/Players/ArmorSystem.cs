using Eternal.Content.Projectiles.Armor;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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

        public static bool StarbornArmorMagicBonus = false;

        int starbornMagicProjTimer = 0;

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

            StarbornArmorMagicBonus = false;
        }

        public override void PreUpdate()
        {
            var entitySource = Player.GetSource_None();

            if (StarbornArmor)
            {
                if (Player.statLifeMax < Player.statLifeMax2 / 2)
                {
                    Player.GetDamage(DamageClass.Generic) += 0.15f;
                }

                if (StarbornArmorMagicBonus)
                {
                    starbornMagicProjTimer++;

                    if (starbornMagicProjTimer >= 2000)
                    {
                        Projectile.NewProjectile(entitySource, Player.position.X, Player.position.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ModContent.ProjectileType<UnstableStarbornWisp>(), Player.statManaMax2, 0, Player.whoAmI);
                        starbornMagicProjTimer = 0;
                    }
                }
                else
                {
                    starbornMagicProjTimer = 0;
                }
            }
            else
            {
                starbornMagicProjTimer = 0;
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (StarbornArmor)
            {
                if (Player.statLifeMax < Player.statLifeMax2 / 2)
                {
                    Player.HealEffect(15, false);
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
