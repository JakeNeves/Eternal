using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Accessories;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Players
{
    public class AccessorySystem : ModPlayer
    {
        // hell mode
        public static bool Dreadheart = false;
        public static bool BlackCandle = false;

        // expert mode
        public static bool Bloodtooth = false;
        public static bool DuneCore = false;
        public static bool GiftofTheSwordGod = false;

        // misc
        public static bool BlackLantern = false;

        public override void ResetEffects()
        {
            // hell mode
            Dreadheart = false;
            BlackCandle = false;

            // expert mode
            Bloodtooth = false;
            DuneCore = false;
            GiftofTheSwordGod = false;

            // misc
            BlackLantern = false;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            var entitySource = NPC.GetSource_None();

            if (Dreadheart)
            {
                Player.HealEffect(Main.rand.Next(6, 12), false);
            }

            if (Bloodtooth)
            {
                SoundEngine.PlaySound(SoundID.DD2_OgreSpit, Player.Center);
                for (int i = 0; i < Main.rand.Next(4, 8); i++)
                    Projectile.NewProjectile(entitySource, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<BloodtoothProjectile>(), (int)damage, 0f, Main.myPlayer);
            }

            if (DuneCore)
            {
                for (int i = 0; i < Main.rand.Next(8, 16); i++)
                    Projectile.NewProjectile(entitySource, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<DuneSparkFriendly>(), (int)damage, 0f, Main.myPlayer);
            }
        }

        public override void PostUpdate()
        {
            if (BlackLantern)
                if (ModContent.GetInstance<ZoneSystem>().zoneBeneath)
                    Player.buffImmune[BuffID.Obstructed] = true;

            if (BlackCandle)
                if (ModContent.GetInstance<ZoneSystem>().zoneBeneath)
                {
                    Player.buffImmune[BuffID.Obstructed] = true;
                    Player.buffImmune[BuffID.Blackout] = true;
                    Player.buffImmune[BuffID.Darkness] = true;
                }
        }
    }
}
