using Eternal.Items;
using Eternal.NPCs;
using Eternal.Projectiles.Accessories;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
    public class EternalPlayer : ModPlayer
    {

        public int cosmicApparitionPresence = 0;

        bool justSpawnedIceWheel = false;

        #region Defbuffs
        public bool ominousPresence = false;
        public bool doomFire = false;
        public bool embericCombustion = false;
        #endregion

        #region Minions
        public bool cEnergy = false;
        #endregion

        #region Accessory Effects
        public static bool frostKingsCore = false;
        #endregion

        public bool ZoneLabrynth = false;
		public bool ZoneThunderduneBiome = false;
		public bool ZoneCommet = false;
        public bool ZoneBeneath = false;
        public bool ZoneAshpit = false;

        public bool droxEvent = false;
        public int droxEventPoints = 0;

        public const int maxLifrFruits = 5;
        public int lifeFruits;

        public override void ResetEffects()
        {
            EternalGlobalProjectile.cometGauntlet = false;
            EternalGlobalProjectile.emperorsGift = false;
            frostKingsCore = false;
        }

        public override void PreUpdate()
        {
            if (EternalWorld.hellMode)
            {
                if (!EternalWorld.downedCosmicApparition && NPC.downedMoonlord && !NPC.AnyNPCs(ModContent.NPCType<NPCs.Boss.CosmicApparition.CosmicApparition>()))
                {
                    cosmicApparitionPresence++;
                    switch (cosmicApparitionPresence)
                    {
                        case 4000:
                            Main.NewText("An unknown halucination manifests and seeks your soul...", 220, 0, 210);
                            break;
                        case 8000:
                            Main.NewText("Shrieks of fallen lives start to echo around you...", 220, 0, 210);
                            break;
                        case 12000:
                            Main.NewText("Something approaches from a vast distance...", 220, 0, 210);
                            break;
                        case 16000:
                            NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<NPCs.Boss.CosmicApparition.CosmicApparition>());
                            Main.NewText("A Cosmic Apparition has awoken!", 175, 75, 255);
                            cosmicApparitionPresence = 0;
                            break;
                    }
                }
                else
                {
                    cosmicApparitionPresence = 0;
                }
            }
            else
            {
                cosmicApparitionPresence = 0;
            }
        }

        public override void PostUpdate()
        {
            if (frostKingsCore)
            {
                if (!justSpawnedIceWheel)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, ModContent.ProjectileType<IceWheelProjectile>(), 120, 1, Main.myPlayer, 0, 0);
                    justSpawnedIceWheel = true;
                }
            }
            else
            {
                justSpawnedIceWheel = false;
            }

            if (ZoneBeneath)
            {
                player.AddBuff(BuffID.Obstructed, 1);
            }
        }

        public override void UpdateDead()
        {
            doomFire = false;
            embericCombustion = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (doomFire)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 20;
                if (player.statLife <= 0)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " had there soul consumed by the wrath of Incinerius"), 10000, 1, false);
                }
            }
            else if (embericCombustion)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 15;
                if (player.statLife <= 0)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "' was cremated into ashes"), 10000, 1, false);
                }
            }
        }

        public override void UpdateBiomeVisuals()
        {
            bool ashpitBiome = (ZoneAshpit);

            player.ManageSpecialBiomeVisuals("Eternal:AshpitSky", ashpitBiome);
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			Item item = new Item();
			item.SetDefaults(ModContent.ItemType<StartingBag>());
            items.Add(item);
		}

        public override void UpdateBiomes()
        {
			ZoneThunderduneBiome = EternalWorld.thunderduneBiome > 100;
			ZoneCommet = EternalWorld.commet > 20;
            ZoneLabrynth = EternalWorld.labrynth > 50;
            ZoneBeneath = EternalWorld.theBeneath > 50;
            ZoneAshpit = EternalWorld.ashpit > 70;
        }
    }
}
