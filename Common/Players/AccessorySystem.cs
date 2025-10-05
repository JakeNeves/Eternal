using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.Ammo;
using Eternal.Content.NPCs.Misc;
using Eternal.Content.Projectiles.Accessories;
using Eternal.Content.Projectiles.Armor;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
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
        public static bool GasBag = false;
        public static bool AstralCompensator = false;
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
        public static bool hasJakesCursor = false;

        // wings
        public static bool CosmicStarstryderTreads = false;
        public static bool RoyalKeepersTreads = false;

        public override void ResetEffects()
        {
            // hell mode
            Dreadheart = false;
            BlackCandle = false;

            // expert mode
            Bloodtooth = false;
            DuneCore = false;
            GasBag = false;
            AstralCompensator = false;
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
            hasJakesCursor = false;

            // wings
            CosmicStarstryderTreads = false;
            RoyalKeepersTreads = false;
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
                }

                for (int j = 0; j < Main.rand.Next(2, 4); j++)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_Bloodtooth") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<BloodtoothProjectile>(), info.Damage, 0f);
                }
            }

            if (GasBag)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active)
                        continue;
                }

                for (int j = 0; j < Main.rand.Next(4, 8); j++)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_GasBag") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<GasCloudFriendly>(), 0, 0f);
                }
            }

            if (ShadeLocket)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active)
                        continue;
                }

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ShademanTrap")
                    {
                        Volume = 0.8f,
                        PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                        MaxInstances = 0
                    });

                    Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_ShadeLocket") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, 12f, 0f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f);
                    Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_ShadeLocket") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, 0f, 12f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f);
                    Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_ShadeLocket") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, -12f, 0f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f);
                    Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_ShadeLocket") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, 0f, -12f, ModContent.ProjectileType<ShadeBombFriendly>(), info.Damage, 0f);
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
                            Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_DuneCore") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, Main.rand.Next(-8, 8), Main.rand.Next(-8, 8), ModContent.ProjectileType<DuneSparkFriendly>(), 24, 0f);
                    }
                }
            }
        }

        public override void PreUpdate()
        {
            if (GasBag)
            {
                if (Main.rand.NextBool(24) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(Player.GetSource_FromThis("Accessory_GasBag") /*Player.GetSource_Accessory_OnHurt(Main.item[ModContent.ItemType<ShadeLocket>()], npc)*/, Player.Center.X, Player.Center.Y, Main.rand.Next(-2, 2), -4, ModContent.ProjectileType<GasCloudFriendly>(), 0, 0f);
                }
            }

            if (AstralCompensator)
            {
                if (Main.rand.NextBool(48) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        NPC.NewNPC(Player.GetSource_FromThis("Accessory_GasBag"), (int)Player.Center.X + Main.rand.Next(-100, 100), (int)Player.Center.Y + Main.rand.Next(-100, 100), ModContent.NPCType<AstralWisp>());
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

            if (CosmicStarstryderTreads)
            {
                Dust dust;
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Dust.NewDust(Player.position, (int)Player.width, (int)Player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 0.75f)];
                dust.fadeIn = 0.4f;
                dust.noGravity = true;
            }

            if (RoyalKeepersTreads)
            {
                Dust dust;
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Dust.NewDust(Player.position, (int)Player.width, (int)Player.height, ModContent.DustType<PsycheFire>(), 0f, 0f, 0, new Color(255, 255, 255), 0.75f)];
                dust.fadeIn = 0.4f;
                dust.noGravity = true;
            }

            if (hasVoidCursor)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.Black, Color.Red, Color.Black]);
            }

            if (hasCursorofTheCosmos)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.Purple, Color.Magenta, Color.Purple]);
            }

            if (hasSpiritArkCursor)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.Teal, Color.Green, Color.Teal]);
            }

            if (hasTheEternalCursor)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, [Color.DarkRed, Color.PaleVioletRed, Color.DarkRed]);
            }

            if (hasJakesCursor)
            {
                Main.cursorColor = EternalCommonUtils.MultiLerpColor(Main.LocalPlayer.miscCounter % 150 / 150f, [Color.HotPink, Color.Yellow, Color.Turquoise, Color.Yellow, Color.HotPink]);
            }
        }
    }
}
