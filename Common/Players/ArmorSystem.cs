using Eternal.Content.Projectiles.Armor;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class ArmorSystem : ModPlayer
    {
        public static bool SubzeroArmor = false;
        public static bool IgneousArmor = false;
        public static bool DuneshockArmor = false;
        public static bool CosmicKeeperArmor = false;
        public static bool IesniumArmor = false;

        #region Occulist Armor
        public static bool OcculistArmor = false;
        public static bool OcculistArmorMagicBonus = false;
        public static bool OcculistArmorSummonBonus = false;
        #endregion

        #region Sanguine Armor
        public static bool SanguineArmor = false;
        public static bool SanguineArmorMeleeBonus = false;
        public static bool SanguineArmorRangedBonus = false;
        #endregion

        #region Starborn Armor
        public static bool StarbornArmor = false;
        public static bool StarbornArmorMagicBonus = false;
        public static bool StarbornArmorMeleeBonus = false;
        public static bool StarbornArmorRangedBonus = false;
        public static bool StarbornArmorSummonBonus = false;
        public static bool StarbornArmorRadiantBonus = false;
        #endregion

        #region Arkanium Armor
        public static bool ArkaniumArmor = false;
        public static bool ArkaniumArmorMagicBonus = false;
        public static bool ArkaniumArmorMeleeBonus = false;
        public static bool ArkaniumArmorRangedBonus = false;
        public static bool ArkaniumArmorSummonBonus = false;
        public static bool ArkaniumArmorRadiantBonus = false;
        #endregion

        #region Ultimus Armor
        public static bool UltimusArmor = false;
        public static bool UltimusArmorMagicBonus = false;
        public static bool UltimusArmorMeleeBonus = false;
        public static bool UltimusArmorRangedBonus = false;
        public static bool UltimusArmorSummonBonus = false;
        public static bool UltimusArmorRadiantBonus = false;
        #endregion

        #region Naquadah Armor
        public static bool NaquadahArmor = false;
        public static bool NaquadahArmorMagicBonus = false;
        public static bool NaquadahArmorMeleeBonus = false;
        public static bool NaquadahArmorRangedBonus = false;
        public static bool NaquadahArmorSummonBonus = false;
        public static bool NaquadahArmorRadiantBonus = false;
        #endregion

        int starbornMagicProjTimer = 0;

        public override void ResetEffects()
        {
            SanguineArmor = false;
            SanguineArmorMeleeBonus = false;
            SanguineArmorRangedBonus = false;

            OcculistArmor = false;
            OcculistArmorMagicBonus = false;
            OcculistArmorSummonBonus = false;

            StarbornArmor = false;
            StarbornArmorMagicBonus = false;
            StarbornArmorMeleeBonus = false;
            StarbornArmorRangedBonus = false;
            StarbornArmorSummonBonus = false;
            StarbornArmorRadiantBonus = false;

            ArkaniumArmor = false;
            ArkaniumArmorMagicBonus = false;
            ArkaniumArmorMeleeBonus = false;
            ArkaniumArmorRangedBonus = false;
            ArkaniumArmorSummonBonus = false;
            ArkaniumArmorRadiantBonus = false;

            UltimusArmor = false;
            UltimusArmorMagicBonus = false;
            UltimusArmorMeleeBonus = false;
            UltimusArmorRangedBonus = false;
            UltimusArmorSummonBonus = false;
            UltimusArmorRadiantBonus = false;

            NaquadahArmor = false;
            NaquadahArmorMagicBonus = false;
            NaquadahArmorMeleeBonus = false;
            NaquadahArmorRangedBonus = false;
            NaquadahArmorSummonBonus = false;
            NaquadahArmorRadiantBonus = false;

            SubzeroArmor = false;

            IgneousArmor = false;

            DuneshockArmor = false;
            CosmicKeeperArmor = false;
            IesniumArmor = false;
        }

        public override void PreUpdate()
        {
            #region Starborn Armor Set Bonuses
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
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Player.GetSource_FromThis("SetBonus_StarbornArmorMagicBonus"), Player.position.X, Player.position.Y, Main.rand.Next(-4, 4), Main.rand.Next(-4, 4), ModContent.ProjectileType<UnstableStarbornWisp>(), Player.statManaMax2, 0, Player.whoAmI);

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
            #endregion
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            #region Starborn Armor Set Bonuses
            if (StarbornArmor)
            {
                if (Player.statLifeMax < Player.statLifeMax2 / 2)
                {
                    Player.HealEffect(15, false);
                }
            }
            #endregion
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            var entitySource = Player.GetSource_FromThis();
                
            #region Arkanium Aromr Set Bonuses
            if (ArkaniumArmor)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active)
                        continue;

                    for (int j = 0; j < Main.rand.Next(4, 8); j++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Player.GetSource_OnHurt(npc, "SetBonus_ArkiumArmor"), Player.position.X + Main.rand.NextFloat(-200f, 200f), Player.position.Y - 600f, Main.rand.NextFloat(-4f, 4f), 16f, ModContent.ProjectileType<ArkaniumSword>(), info.Damage * 2, 0, Player.whoAmI);
                    }
                }
            }
            #endregion

            #region Naquadah Armor Set Bonuses
            if (NaquadahArmor)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.DD2_KoboldExplosion, Player.position);

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active)
                        continue;


                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(Player.GetSource_OnHurt(npc, "SetBonus_NaquadahArmor"), Player.position, new Vector2(0, 0), ModContent.ProjectileType<NaquadahSpikeBombAOE>(), 0, 0, Player.whoAmI);

                    for (int j = 0; j < Main.rand.Next(2, 4); j++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Player.GetSource_OnHurt(npc, "SetBonus_NaquadahArmor"), Player.position, new Vector2(Main.rand.NextFloat(-8f, 8f), Main.rand.NextFloat(-8f, 8f)), ModContent.ProjectileType<NaquadahSpikeBomb>(), info.Damage * 2, 0, Player.whoAmI);
                    }
                }
            }
            #endregion
        }
    }
}
