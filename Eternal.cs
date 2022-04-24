using Eternal.Items.Accessories.Expert;
using Eternal.Items.Armor;
using Eternal.Items.BossBags;
using Eternal.Items.Materials;
using Eternal.Items.Materials.Elementalblights;
using Eternal.Items.Placeable;
using Eternal.Items.Potions;
using Eternal.Items.Summon;
using Eternal.Items.Tools;
using Eternal.Items.Weapons.Expert;
using Eternal.Items.Weapons.Magic;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Radiant;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.Weapons.Summon;
using Eternal.Items.Weapons.Throwing;
using Eternal.Skies;
using Eternal.UI;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Eternal
{
    public class Eternal : Mod
    {
        internal static Eternal instance;

        private UserInterface _etherealPowerBarUserInterface;

        internal EtherealPowerBar EtherealPowerBar;

        internal bool CalamityLoaded;
        internal bool FargowiltasModLoaded;

        public static int ApollyonCoin = -1;

        public Eternal()
        {

            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };

        }

        public override void AddRecipeGroups()
        {
            RecipeGroup celestialFragment = new RecipeGroup(() => "any" + " Celestial Fragment", new int[]
            {
                ItemID.FragmentNebula,
                ItemID.FragmentVortex,
                ItemID.FragmentSolar,
                ItemID.FragmentStardust
            });

            RecipeGroup adamantanium = new RecipeGroup(() => "Any" + " Adamantite Bar", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });

            RecipeGroup adamantaniumForge = new RecipeGroup(() => "Any" + " Adamantite Forge", new int[]
            {
                ItemID.AdamantiteForge,
                ItemID.TitaniumForge
            });

            RecipeGroup mythrilAnvil = new RecipeGroup(() => "Any" + " Mythril Anvil", new int[]
            {
                ItemID.MythrilAnvil,
                ItemID.OrichalcumAnvil
            });

            RecipeGroup gemStaff = new RecipeGroup(() => "Any" + " Gem Staff", new int[]
            {
                ItemID.AmberStaff,
                ItemID.RubyStaff,
                ItemID.DiamondStaff,
                ItemID.AmethystStaff,
                ItemID.EmeraldStaff,
                ItemID.SapphireStaff,
                ItemID.TopazStaff,
            });

            RecipeGroup grimstone = new RecipeGroup(() => "Any" + " Grimstone", new int[]
            {
                ModContent.ItemType<Grimstone>(),
                ModContent.ItemType<Darkslate>()
            });

            RecipeGroup.RegisterGroup("Eternal:CelestialFragment", celestialFragment);
            RecipeGroup.RegisterGroup("Eternal:Adamantanium", adamantanium);
            RecipeGroup.RegisterGroup("Eternal:AdamantaniumForge", adamantaniumForge);
            RecipeGroup.RegisterGroup("Eternal:MythrilAnvil", mythrilAnvil);
            RecipeGroup.RegisterGroup("Eternal:GemStaff", gemStaff);
            RecipeGroup.RegisterGroup("Eternal:Grimstone", grimstone);
        }

        public override void Unload()
        {
            instance = null;
        }

        public override void Load()
        {
            instance = this;

            ApollyonCoin = CustomCurrencyManager.RegisterCurrency(new Items.ACoins(ModContent.ItemType<Items.ApollyonCoin>()));

            if (Main.netMode != NetmodeID.Server)
            {
                #region sky things
                Filters.Scene["Eternal:Empraynia"] = new Filter(new EmprayniaScreenShaderData("FilterMiniTower").UseColor(0.229f, 0.84f, 0.255f).UseOpacity(0.6f), EffectPriority.VeryHigh);
                SkyManager.Instance["Eternal:Empraynia"] = new EmprayniaSky();

                Filters.Scene["Eternal:AshpitSky"] = new Filter((new ScreenShaderData("FilterMiniTower")).UseColor(0f, 0f, 0f).UseOpacity(0f), EffectPriority.VeryLow);
                SkyManager.Instance["Eternal:AshpitSky"] = new AshpitSky();

                Filters.Scene["Eternal:CosmicEmperorP3"] = new Filter(new EmprayniaScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(0f), EffectPriority.VeryHigh);
                SkyManager.Instance["Eternal:CosmicEmperorP3"] = new EmprayniaSky();
                #endregion
            }

            #region Radiant Class Bar
            EtherealPowerBar = new EtherealPowerBar();
            _etherealPowerBarUserInterface = new UserInterface();
            _etherealPowerBarUserInterface.SetState(EtherealPowerBar);
            #endregion
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _etherealPowerBarUserInterface?.Update(gameTime);
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.invasionX == Main.spawnTileX)
            {
                if (EternalWorld.mechanicalEnvyUp)
                    music = MusicID.MartianMadness;
            }

            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }
            if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneLabrynth)
            {
                music = MusicID.Dungeon;
                priority = MusicPriority.BiomeMedium;
            }

            if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneCommet)
            {
                music = MusicID.Eerie;
                priority = MusicPriority.BiomeMedium;
            }

            if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneThunderduneBiome)
            {
                music = MusicID.Desert;
                priority = MusicPriority.BiomeMedium;
            }

            if (Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneAshpit || Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneBeneath)
            {
                music = MusicID.Eerie;
                priority = MusicPriority.BiomeMedium;
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int ethPowerBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (ethPowerBarIndex != 1)
            {
                layers.Insert(ethPowerBarIndex, new LegacyGameInterfaceLayer(
                    "Eternal: Ethereal Power Bar",
                    delegate
                    {
                        _etherealPowerBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }


        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) &&
                (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

        public override void PostSetupContent()
        {
            try
            {
                //CalamityIntegration = new CalamityIntegration(this).TryLoad() as CalamityIntegration;
                //FargoModIntegration = new FargoModIntegration(this).TryLoad() as FargoModIntegration;

                CalamityLoaded = ModLoader.GetMod("CalamityMod") != null;
                FargowiltasModLoaded = ModLoader.GetMod("FargowiltasSouls") != null;
            }
            catch (Exception e)
            {
                Logger.Warn("Eternal PostSetupContent Error: " + e.StackTrace + e.Message);
            }

            #region Boss Checklist Integration
            Mod bossCheckList = ModLoader.GetMod("BossCheckList");
            if (bossCheckList != null)
            {
                //Pre-Hardmode
                bossCheckList.Call(
                    "AddBoss",
                    5.4f,
                    ModContent.NPCType<NPCs.Boss.CarmaniteScouter.CarmaniteScouter>(),
                    this,
                    "The Carmanite Scouter",
                    (Func<bool>)(() => EternalWorld.downedCarmaniteScouter),
                    ModContent.ItemType<SuspiciousLookingDroplet>(),
                    0,
                    new List<int> { ModContent.ItemType<CarmaniteScouterBag>(), ModContent.ItemType<Bloodtooth>(), ModContent.ItemType<Carmanite>(), ModContent.ItemType<CarmaniteBane>(), ModContent.ItemType<CarmaniteRipperClaws>(), ModContent.ItemType<BruteCleavage>(), ModContent.ItemType<CarmanitePurgatory>(), ModContent.ItemType<CarmaniteDeadshot>(), ItemID.LesserHealingPotion },
                    "Spawn by using the [i:" + ModContent.ItemType<SuspiciousLookingDroplet>() + "]",
                    "The Blood-Feasting Amalgamate.",
                    "Eternal/BossChecklist/CarmaniteScouter",
                    "Eternal/NPCs/Boss/CarmaniteScouter/CarmaniteScouter_Head_Boss"
                );

                bossCheckList.Call(
                    "AddBoss",
                    5.6f,
                    ModContent.NPCType<NPCs.Boss.Dunekeeper.Dunekeeper>(),
                    this,
                    "Dunekeeper",
                    (Func<bool>)(() => EternalWorld.downedDunekeeper),
                    ModContent.ItemType<RuneofThunder>(),
                    0,
                    new List<int> { ModContent.ItemType<DunekeeperBag>(), ModContent.ItemType<PrimordialBolt>(), ModContent.ItemType<ThunderblightCrystal>(), ModContent.ItemType<StormBeholder>(), ModContent.ItemType<ThunderduneHeadgear>(), ModContent.ItemType<Wasteland>(), ItemID.LesserHealingPotion },
                    "Spawn by using the [i:" + ModContent.ItemType<RuneofThunder>() + "] in the desert.",
                    "The Unstabe Thundergen of the Desert.",
                    "Eternal/BossChecklist/Dunekeeper",
                    "Eternal/NPCs/Boss/Dunekeeper/Dunekeeper_Head_Boss"
                );

                //Hardmode
                bossCheckList.Call(
                    "AddBoss",
                    7f,
                    ModContent.NPCType<NPCs.Boss.Incinerius.Incinerius>(),
                    this,
                    "Incinerius",
                    (Func<bool>)(() => EternalWorld.downedIncinerius),
                    ModContent.ItemType<RelicofInferno>(),
                    0,
                    new List<int> { ModContent.ItemType<IncineriusBag>(), ModContent.ItemType<FlameInfusedJewel>(), ModContent.ItemType<ScorchedMetal>(), ModContent.ItemType<SmotheringInferno>(), ModContent.ItemType<FuryFlare>(), ModContent.ItemType<Pyroyo>(), ItemID.GreaterHealingPotion },
                    "Spawn by using the [i:" + ModContent.ItemType<RelicofInferno>() + "] in the underworld.",
                    "The Flaming Golem of the Underworld",
                    "Eternal/BossChecklist/Incinerius",
                    "Eternal/NPCs/Boss/Incinerius/Incinerius_Head_Boss"
                );

                bossCheckList.Call(
                    "AddBoss",
                    10.6f,
                    ModContent.NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>(),
                    this,
                    "Subzero Elemental",
                    (Func<bool>)(() => EternalWorld.downedSubzeroElemental),
                    ModContent.ItemType<AncientGlacialInscription>(),
                    0,
                    new List<int> { ModContent.ItemType<SubzeroElementalBag>(), ModContent.ItemType<FrostKingsCore>(), ModContent.ItemType<TheKelvinator>(), ModContent.ItemType<FrostGladiator>(), ModContent.ItemType<FrostyImmaterializer>(), ItemID.GreaterHealingPotion },
                    "Spawn by using the [i:" + ModContent.ItemType<AncientGlacialInscription>() + "] in the snow biome.",
                    "The Living Kelvin Construct",
                    "Eternal/BossChecklist/SubzeroElemental",
                    "Eternal/NPCs/Boss/SubzeroElemental/SubzeroElemental_Head_Boss"
                );

                //Post-Moon Lord
                bossCheckList.Call(
                    "AddBoss",
                    15f,
                    ModContent.NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>(),
                    this,
                    "Subzero Elemental (Post-Moon Lord)",
                    (Func<bool>)(() => EternalWorld.downedSubzeroElementalP2),
                    ModContent.ItemType<AncientGlacialInscription>(),
                    0,
                    new List<int> { ModContent.ItemType<SubzeroElementalBag>(), ModContent.ItemType<FrostKingsCore>(), ModContent.ItemType<TheKelvinator>(), ModContent.ItemType<FrostGladiator>(), ModContent.ItemType<FrostyImmaterializer>(), ModContent.ItemType<SydaniteOre>(), ModContent.ItemType<Frostpike>(), ModContent.ItemType<FrostDisk>(), ItemID.SuperHealingPotion },
                    "Refight the Subzero Elemental Post-Moon Lord.",
                    "The Living Kelvin Construct is Back",
                    "Eternal/BossChecklist/SubzeroElemental",
                    "Eternal/NPCs/Boss/SubzeroElemental/SubzeroElemental_Head_Boss"
                );

                bossCheckList.Call(
                    "AddBoss",
                    15.5f,
                    ModContent.NPCType<NPCs.Boss.CosmicApparition.CosmicApparition>(),
                    this,
                    "Cosmic Apparition",
                    (Func<bool>)(() => EternalWorld.downedCosmicApparition),
                    ModContent.ItemType<OtherworldlyDebris>(),
                    0,
                    new List<int> { ModContent.ItemType<CosmicApparitionBag>(), ModContent.ItemType<ResonantPhantasmoblood>(), ModContent.ItemType<ApparitionalRendingStaff>(), ModContent.ItemType<ApparitionalDisk>(), ModContent.ItemType<Vexation>(), ModContent.ItemType<Cometstorm>(), ModContent.ItemType<PristineHealingPotion>() },
                    "Spawn by using a [i:" + ModContent.ItemType<OtherworldlyDebris>() + "] in the comet biome",
                    "The Ghost of Someone Powerful",
                    "Eternal/BossChecklist/CosmicApparition",
                    "Eternal/NPCs/Boss/CosmicApparition/CosmicApparition_Head_Boss"
                );

                bossCheckList.Call(
                    "AddBoss",
                    15.75f,
                    ModContent.NPCType<NPCs.Boss.AoI.ArkofImperious>(),
                    this,
                    "Ark of Imperious",
                    (Func<bool>)(() => EternalWorld.downedArkOfImperious),
                    ModContent.ItemType<RoyalShrineSword>(),
                    0,
                    new List<int> { ModContent.ItemType<AoIBag>(), ModContent.ItemType<GiftofTheSwordGod>(), ModContent.ItemType<TheImperiousCohort>(), ModContent.ItemType<TheEnigma>(), ModContent.ItemType<DormantHeroSword>(), ModContent.ItemType<Arkbow>(), ModContent.ItemType<PristineHealingPotion>() },
                    "Spawn by using the [i:" + ModContent.ItemType<RoyalShrineSword>() + "] at the shrine",
                    "The mighty imperial blade of the shrine",
                    "Eternal/BossChecklist/ArkofImperious",
                    "Eternal/NPCs/Boss/AoI/ArkofImperious_Head_Boss"
                );

                bossCheckList.Call(
                    "AddBoss",
                    20.05f,
                    ModContent.NPCType<NPCs.Boss.CosmicEmperor.CosmicEmperorP3>(),
                    this,
                    "The Cosmic Emperor",
                    (Func<bool>)(() => EternalWorld.downedCosmicEmperor),
                    ModContent.ItemType<CosmicTablet>(),
                    new List<int> { },
                    new List<int> { ModContent.ItemType<CosmoniumFragment>(), ModContent.ItemType<Exelodon>(), ModContent.ItemType<TheBigOne>(), ModContent.ItemType<ExosiivaGladiusBlade>(), ModContent.ItemType<StarcrescentMoondisk>(), ModContent.ItemType<PristineHealingPotion>() },
                    "Spawn by using the [i:" + ModContent.ItemType<CosmicTablet>() + "] after defeating the Ark of Imperious",
                    "The almighty powerful emperor of the cosmos.",
                    "Eternal/BossChecklist/CosmicEmperor",
                    "Eternal/NPCs/Boss/CosmicEmperor/CosmicEmperor_Head_Boss"
                );

            }
            #endregion
            #region YABHBM Integration
            Mod FKBossHealthBar = ModLoader.GetMod("FKBossHealthBar");
            if (FKBossHealthBar != null)
            {
                // Incinerius
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/IncineriusBarStart"),
                    GetTexture("BossBars/IncineriusBarMiddle"),
                    GetTexture("BossBars/IncineriusBarEnd"),
                    GetTexture("BossBars/IncineriusBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/Incinerius/Incinerius_Head_Boss"));
                FKBossHealthBar.Call("hbSetColours",
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.Incinerius.Incinerius>());
                // Subzero Elemental
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/SubzeroElementalBarStart"),
                    GetTexture("BossBars/SubzeroElementalBarMiddle"),
                    GetTexture("BossBars/SubzeroElementalBarEnd"),
                    GetTexture("BossBars/SubzeroElementalBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/SubzeroElemental/SubzeroElemental_Head_Boss"));
                FKBossHealthBar.Call("hbSetColours",
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.SubzeroElemental.SubzeroElemental>());
                // Bionic Bosses
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Atlas_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Atlas>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Borealis_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Borealis>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Photon_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Photon>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Proton_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Proton>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Quasar_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Quasar>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Orion_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Orion>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Polarus_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Polarus>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/BionicBossBarStart"),
                    GetTexture("BossBars/BionicBossBarMiddle"),
                    GetTexture("BossBars/BionicBossBarEnd"),
                    GetTexture("BossBars/BionicBossBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/BionicBosses/Omnicron/Omnicron_Head_Boss"));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.BionicBosses.Omnicron.Omnicron>());
                // AoI
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/AoIBarStart"),
                    GetTexture("BossBars/AoIBarMiddle"),
                    GetTexture("BossBars/AoIBarEnd"),
                    GetTexture("BossBars/AoIBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/AoI/ArkofImperious_Head_Boss"));
                FKBossHealthBar.Call("hbSetColours",
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.AoI.ArkofImperious>());
                // Cosmic Emperor
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/CosmicEmperorBarStart"),
                    GetTexture("BossBars/CosmicEmperorBarMiddle"),
                    GetTexture("BossBars/CosmicEmperorBarEnd"),
                    GetTexture("BossBars/CosmicEmperorBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/CosmicEmperor/CosmicEmperor_Head_Boss"));
                FKBossHealthBar.Call("hbSetColours",
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.CosmicEmperor.CosmicEmperor>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/CosmicEmperorBarStart"),
                    GetTexture("BossBars/CosmicEmperorBarMiddle"),
                    GetTexture("BossBars/CosmicEmperorBarEnd"),
                    GetTexture("BossBars/CosmicEmperorBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/CosmicEmperor/CosmicEmperorMask_Head_Boss"));
                FKBossHealthBar.Call("hbSetColours",
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.CosmicEmperor.CosmicEmperorMask>());
                FKBossHealthBar.Call("hbStart");
                FKBossHealthBar.Call("hbSetTexture",
                    GetTexture("BossBars/CosmicEmperorBarStart"),
                    GetTexture("BossBars/CosmicEmperorBarMiddle"),
                    GetTexture("BossBars/CosmicEmperorBarEnd"),
                    GetTexture("BossBars/CosmicEmperorBarFill"));
                FKBossHealthBar.Call("hbSetBossHeadTexture", GetTexture("NPCs/Boss/CosmicEmperor/CosmicEmperorP3_Head_Boss"));
                FKBossHealthBar.Call("hbSetColours",
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f),
                    new Color(1f, 1f, 1f));
                FKBossHealthBar.Call("hbFinishSingle", ModContent.NPCType<NPCs.Boss.CosmicEmperor.CosmicEmperorP3>());
            }
            #endregion
        }

    }
}