﻿using Eternal.Content.Items.Summon;
using Eternal.Content.NPCs.Boss.AoI;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Boss.CosmicEmperor;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Eternal.Content.NPCs.Boss.Duneworm;
using Eternal.Content.NPCs.Boss.Igneopede;
using Eternal.Content.NPCs.Boss.Incinerius;
using Eternal.Content.NPCs.Miniboss;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    public class IntegrationSystem : ModSystem
    {
        public override void PostSetupContent()
        {
			BossChecklistIntegration();
        }

        private void BossChecklistIntegration()
        {
			if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
			{
				return;
			}

            #region Pre-Hardmode
            #region Carminite Amalgamation
            bossChecklistMod.Call(
				"LogBoss",
				Mod,
				nameof(CarminiteAmalgamation),
				4.5f,
				() => DownedBossSystem.downedCarminiteAmalgamation,
				ModContent.NPCType<CarminiteAmalgamation>(),
				new Dictionary<string, object>()
				{
					["spawnItems"] = ModContent.ItemType<SuspiciousLookingDroplet>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.CarminiteAmalgamation.DisplayName"),
					["collectables"] = new List<int>()
					{
					    ModContent.ItemType<Content.Items.Materials.Carminite>(),
					    ModContent.ItemType<Content.Items.Weapons.Melee.CarminiteBroadsword>(),
					    ModContent.ItemType<Content.Items.Weapons.Melee.CarminiteRipperClaws>(),
						ModContent.ItemType<Content.Items.Weapons.Melee.CarminiteShortsword>(),
						ModContent.ItemType<Content.Items.Weapons.Ranged.CarminiteShortbow>()
					}
                }
			);
            #endregion

            #region Dune Golem
            bossChecklistMod.Call(
				"LogBoss",
				Mod,
				nameof(DuneGolem),
				5.6f,
				() => DownedBossSystem.downedDuneGolem,
				ModContent.NPCType<DuneGolem>(),
				new Dictionary<string, object>()
				{
                    ["spawnItems"] = ModContent.ItemType<IesniumBeacon>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.DuneGolem.DisplayName"),
                    ["collectables"] = new List<int>()
					{
						ModContent.ItemType<Content.Items.Materials.MalachiteShard>()
					}
                }
			);
            #endregion
            #endregion

            #region Hardmode
            #region The Igneopede
            /* bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(IgneopedeHead),
				7.5f,
				() => DownedBossSystem.downedIgneopede,
				ModContent.NPCType<IgneopedeHead>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<MoltenBait>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.IgneopedeHead.DisplayName")
                    /*
                    ["collectables"] = new List<int>()
                    {
                        // TODO: Igneopede Loot
                    }
					
                }
            ); */
            #endregion

            #region Incinerius
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(Incinerius),
                12.25f,
                () => DownedBossSystem.downedIncinerius,
                ModContent.NPCType<Incinerius>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<ObsidianLantern>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.Incinerius.DisplayName"),
                    ["collectables"] = new List<int>()
					{
						ModContent.ItemType<Content.Items.Materials.MagmaticAlloy>(),
						ModContent.ItemType<Content.Items.Materials.InfernalAshes>()
					}
                }
            );
            #endregion

            #region Subzero Elemental
            #endregion

            #region Duneworm
            /* bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(DunewormHead),
                13f,
                () => DownedBossSystem.downedDuneworm,
                ModContent.NPCType<DunewormHead>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<SandFood>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.DunewormHead.DisplayName")
                    /*
                    ["collectables"] = new List<int>()
                    {
                        // TODO: Duneworm Loot
                    }
					
                }
            );*/
            #endregion

            #region Dark Moon (Event)
            #endregion

            #region Empraynia
            #endregion
            #endregion

            #region Post-Moon Lord
            #region Cosmic Apparition
            bossChecklistMod.Call(
				"LogBoss",
				Mod,
				nameof(CosmicApparition),
				19.5f,
				() => DownedBossSystem.downedCosmicApparition,
				ModContent.NPCType<CosmicApparition>(),
                new Dictionary<string, object>()
				{
                    ["spawnItems"] = ModContent.ItemType<OtherworldlyCosmicDebris>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.CosmicApparition.DisplayName"),
                    ["collectables"] = new List<int>()
					{
						ModContent.ItemType<Content.Items.Materials.ApparitionalMatter>(),
						ModContent.ItemType<Content.Items.Materials.Astragel>(),
						ModContent.ItemType<Content.Items.Materials.InterstellarScrapMetal>(),
						ModContent.ItemType<Content.Items.Materials.StarmetalBar>(),
						ModContent.ItemType<Content.Items.Weapons.Melee.Vexation>(),
						ModContent.ItemType<Content.Items.Weapons.Melee.ApparitionalDisk>(),
						ModContent.ItemType<Content.Items.Weapons.Ranged.Starfall>(),
						ModContent.ItemType<Content.Items.Pets.ReminantHead>()
					}
				}
            );
            #endregion

            #region Ark of Imperious
            bossChecklistMod.Call(
				"LogBoss",
				Mod,
				nameof(ArkofImperious),
				20f,
				() => DownedBossSystem.downedArkofImperious,
				ModContent.NPCType<ArkofImperious>(),
                new Dictionary<string, object>()
				{
                    ["spawnItems"] = ModContent.ItemType<RoyalShrineSword>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.ArkofImperious.DisplayName"),
                    ["collectables"] = new List<int>()
					{
						ModContent.ItemType<Content.Items.Materials.ArkiumQuartzCrystalCluster>(),
						ModContent.ItemType<Content.Items.Materials.ArkaniumScrap>(),
						ModContent.ItemType<Content.Items.Materials.WeatheredPlating>(),
						ModContent.ItemType<Content.Items.Materials.UnrefinedHeroSword>(),
					}
				}
            );
            #endregion

            #region Phantom Construct (Miniboss)
            bossChecklistMod.Call(
                "LogMiniBoss",
                Mod,
                nameof(PhantomConstruct),
                20.25f,
                () => DownedMinibossSystem.downedPhantomConstruct,
                ModContent.NPCType<PhantomConstruct>(),
                new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.PhantomConstruct.DisplayName"),
                    ["collectables"] = new List<int>()
                    {
                        ModContent.ItemType<Content.Items.Materials.RawOminaquadite>(),
                        ModContent.ItemType<Content.Items.Weapons.Melee.AspectofTheShiftedWarlock>(),
                        ModContent.ItemType<Content.Items.Weapons.Melee.RiftedBlade>(),
                        ModContent.ItemType<Content.Items.Weapons.Magic.Shiftstorm>(),
                    }
                }
            );
            #endregion

            #region The Trinity
            #endregion

            #region Dark Moon II (Event)
            #endregion

            #region Cosmic Emperor
            /*bossChecklistMod.Call(
				"LogBoss",
				Mod,
				nameof(CosmicEmperor),
				24f,
				() => DownedBossSystem.downedCosmicEmperor,
				ModContent.NPCType<CosmicEmperorP2>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<CosmicTablet>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.CosmicEmperor.DisplayName"),
                    ["collectables"] = new List<int>()
                    {
                        ModContent.ItemType<Content.Items.Materials.CosmicEmperorsInterstellarAlloy>(),
						ModContent.ItemType<Content.Items.Materials.InterstellarMetal>(),
                        ModContent.ItemType<Content.Items.Materials.CosmoniumFragment>(),
                    }
                }
            );*/
            #endregion
            #endregion
        }
    }
}
