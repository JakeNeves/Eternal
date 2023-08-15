using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Projectiles.Accessories;
using Microsoft.Xna.Framework;
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

        // cursors
        public static bool hasVoidCursor = false;
        public static bool hasCursorofTheCosmos = false;
        public static bool hasSpiritArkCursor = false;
        public static bool hasTheEternalCursor = false;

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

            // cursor
            hasVoidCursor = false;
            hasCursorofTheCosmos = false;
            hasSpiritArkCursor = false;
            hasTheEternalCursor = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            var entitySource = Player.GetSource_FromThis();

            if (Dreadheart)
            {
                Player.HealEffect(Main.rand.Next(6, 12), false);
            }

            if (Bloodtooth)
            {
                SoundEngine.PlaySound(SoundID.DD2_OgreSpit, Player.Center);
                for (int i = 0; i < Main.rand.Next(4, 8); i++)
                    Projectile.NewProjectile(entitySource, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<BloodtoothProjectile>(), 24, 0f, Main.myPlayer);
            }

            if (DuneCore)
            {
                for (int i = 0; i < Main.rand.Next(8, 16); i++)
                    Projectile.NewProjectile(entitySource, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<DuneSparkFriendly>(), 24, 0f, Main.myPlayer);
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

            if (hasVoidCursor)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Black, Color.Red, Color.Black);
            }

            if (hasCursorofTheCosmos)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Purple, Color.Magenta, Color.Purple);
            }

            if (hasSpiritArkCursor)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.Teal, Color.Green, Color.Teal);
            }

            if (hasTheEternalCursor)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, Color.DarkRed, Color.PaleVioletRed, Color.DarkRed);
            }
        }
    }
}
