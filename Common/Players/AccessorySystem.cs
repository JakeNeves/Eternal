using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories;
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
        public static bool Godhead = false;

        // misc
        public static bool BlackLantern = false;
        public static bool ShadeLocket = false;

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
            Godhead = false;

            // misc
            BlackLantern = false;
            ShadeLocket = false;

            // cursor
            hasVoidCursor = false;
            hasCursorofTheCosmos = false;
            hasSpiritArkCursor = false;
            hasTheEternalCursor = false;
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (Dreadheart)
            {
                Player.HealEffect(Main.rand.Next(6, 12), false);
            }

            if (Bloodtooth)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.DD2_OgreSpit, Player.Center);

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active)
                        continue;

                    for (int j = 0; j < Main.rand.Next(4, 8); j++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<Bloodtooth>()], npc), Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<BloodtoothProjectile>(), 24, 0f, Main.myPlayer);
                    }
                }
            }

            if (ShadeLocket)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active)
                        continue;

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ShademanTrap")
                        {
                            Volume = 0.8f,
                            PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                            MaxInstances = 0
                        });

                        Projectile.NewProjectile(Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc), Player.Center.X, Player.Center.Y, 12f, 0f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc), Player.Center.X, Player.Center.Y, 0f, 12f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc), Player.Center.X, Player.Center.Y, -12f, 0f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f, Main.myPlayer);
                        Projectile.NewProjectile(Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc), Player.Center.X, Player.Center.Y, 0f, -12f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f, Main.myPlayer);
                    }
                }
            }

            if (DuneCore)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active)
                        continue;

                    for (int j = 0; j < Main.rand.Next(8, 16); j++)
                    {
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<DuneCore>()], npc), Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<DuneSparkFriendly>(), 24, 0f, Main.myPlayer);
                    }
                }
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
