using Eternal.Content.Items.Summon;
using Eternal.Content.NPCs.Boss.AoI;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Boss.CosmicEmperor;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Eternal.Content.NPCs.Boss.Duneworm;
using Eternal.Content.NPCs.Boss.Igneopede;
using Eternal.Content.NPCs.Boss.Incinerius;
using Eternal.Content.NPCs.Boss.Niades;
using Eternal.Content.NPCs.Boss.TheChimera;
using Eternal.Content.NPCs.Boss.TheGlare;
using Eternal.Content.NPCs.Boss.Trinity;
using Eternal.Content.NPCs.DarkMoon;
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
            bossChecklistMod.Call(
                "LogEvent",
                Mod,
                "DarkMoon",
                13.14f,
                () => EventSystem.downedDarkMoon,
                new List<int>()
                {
                    ModContent.NPCType<BloodSlurper>(),
                    ModContent.NPCType<DarkMoonWatcher>(),
                    ModContent.NPCType<HexedSkeleton>(),
                    ModContent.NPCType<Pseyeche>(),
                    ModContent.NPCType<PsycheSkull>(),
                    ModContent.NPCType<PsycheSlime>(),
                    ModContent.NPCType<PsychoZombie>(),
                    ModContent.NPCType<TwistedPsycheSlime>()
                },
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<Animanomicon>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.Events.DarkMoon.DisplayName"),
                    ["spawnInfo"] = Language.GetText("Mods.Eternal.Events.DarkMoon.BossChecklistIntegration.SpawnInfo")
                }
            );
            #endregion

            #region Niades
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(Niades),
                12.15f,
                () => DownedBossSystem.downedNiades,
                ModContent.NPCType<Niades>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<BloodstainedJudgement>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.Niades.DisplayName"),
                    ["despawnMessage"] = Language.GetText("Mods.Eternal.NPCs.Niades.BossChecklistIntegration.DespawnMessage"),
                    ["collectables"] = new List<int>()
                    {
                        ModContent.ItemType<Content.Items.Accessories.Expert.Rosary>(),
                        ModContent.ItemType<Content.Items.Weapons.Melee.SactothsConquest>()
                    }
                }
            );
            #endregion

            #region The Chimera
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(TheChimera),
                12.25f,
                () => DownedBossSystem.downedChimera,
                ModContent.NPCType<TheChimera>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<RottenMeat>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.TheChimera.DisplayName"),
                    ["collectables"] = new List<int>()
                    {
                        ModContent.ItemType<Content.Items.Accessories.Expert.GasBag>(),
                        ModContent.ItemType<Content.Items.Weapons.Melee.RottenFangspear>(),
                        ModContent.ItemType<Content.Items.Weapons.Melee.RottenMeathook>(),
                        ModContent.ItemType<Content.Items.Weapons.Ranged.Necrobow>()
                    }
                }
            );
            #endregion

            #region The Glare
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(TheGlare),
                13.15f,
                () => DownedBossSystem.downedGlare,
                ModContent.NPCType<TheGlare>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<SuspiciousLookingMask>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.TheGlare.DisplayName"),
                    ["despawnMessage"] = Language.GetText("Mods.Eternal.NPCs.TheGlare.BossChecklistIntegration.DespawnMessage"),
                    ["collectables"] = new List<int>()
                    {
                        ModContent.ItemType<Content.Items.Weapons.Magic.UmbralArcanis>()
                    }
                }
            );
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
            bossChecklistMod.Call(
                "LogBoss",
                Mod,
                nameof(TrinityCore),
                21.0f,
                () => DownedBossSystem.downedTrinity,
                new List<int>()
                {
                    ModContent.NPCType<Thunderius>(),
                    ModContent.NPCType<Infernito>(),
                    ModContent.NPCType<Cryota>(),
                    ModContent.NPCType<TrinityCore>()
                },
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<PrimordialRelic>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.TrinityCore.DisplayName"),
                    ["spawnInfo"] = Language.GetText("Mods.Eternal.NPCs.TrinityCore.BossChecklistIntegration.SpawnInfo"),
                    ["collectables"] = new List<int>()
                    {
                        ModContent.ItemType<Content.Items.Materials.ProviditeBar>(),
                        ModContent.ItemType<Content.Items.Materials.MindCrystalCluster>(),
                        ModContent.ItemType<Content.Items.Materials.BodyCrystalCluster>(),
                        ModContent.ItemType<Content.Items.Materials.SoulCrystalCluster>()
                    }
                }
            );
            #endregion

            #region Dark Moon II (Event)
            bossChecklistMod.Call(
                "LogEvent",
                Mod,
                "DarkMoon2",
                21.25f,
                () => EventSystem.downedDarkMoon2,
                new List<int>()
                {
                    ModContent.NPCType<Shademan>(),
                    ModContent.NPCType<SanguineSphere>(),
                    ModContent.NPCType<ShadeBat>()
                },
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<Animanomicon>(),
                    ["displayName"] = Language.GetText("Mods.Eternal.Events.DarkMoon2.DisplayName"),
                    ["spawnInfo"] = Language.GetText("Mods.Eternal.Events.DarkMoon2.BossChecklistIntegration.SpawnInfo")
                }
            );
            #endregion

            #region Shade Slime (Miniboss)
            bossChecklistMod.Call(
                "LogMiniBoss",
                Mod,
                nameof(ShadeSlime),
                21.28f,
                () => DownedMinibossSystem.downedShadeSlime,
                ModContent.NPCType<ShadeSlime>(),
                new Dictionary<string, object>()
                {
                    ["displayName"] = Language.GetText("Mods.Eternal.NPCs.ShadeSlime.DisplayName"),
                    ["collectables"] = new List<int>()
                    {
                        ModContent.ItemType<Content.Items.Accessories.ShadeLocket>(),
                        ModContent.ItemType<Content.Items.Weapons.Magic.DarkArts>(),
                    }
                }
            );
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
