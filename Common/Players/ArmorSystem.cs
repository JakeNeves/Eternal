using Eternal.Content.Projectiles.Armor;
using Terraria;
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
        public static bool NaquadahArmor = false;

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
            NaquadahArmor = false;

            StarbornArmorMagicBonus = false;
        }

        public override void PreUpdate()
        {
            var entitySource = Player.GetSource_FromThis();

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

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
        {
            if (StarbornArmor)
            {
                if (Player.statLifeMax < Player.statLifeMax2 / 2)
                {
                    Player.HealEffect(15, false);
                }
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            var entitySource = Player.GetSource_FromThis();

            if (ArkaniumArmor)
            {
                for (int i = 0; i < Main.rand.Next(8, 16); i++)
                    Projectile.NewProjectile(entitySource, Player.position.X + Main.rand.Next(-200, 200), Player.position.Y - 600, Main.rand.Next(-4, 4), 16, ModContent.ProjectileType<ArkaniumSword>(), 200 * 2, 0, Player.whoAmI);
            }
        }
    }
}
