using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Potions;
using Eternal.Content.Items.Summon;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Eternal.Content.NPCs.Boss.Igneopede;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
	public class Eternal : Mod
	{
        internal static Eternal instance;

        public const string AssetPath = $"{nameof(Eternal)}/Assets/";

        public override void Load()
        {
            instance = this;
        }

		public override void Unload()
        {
            instance = null;
        }

        public override void PostSetupContent()
        {
            #region Boss Checklist Integration
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                // pre-hardmode
                bossChecklist.Call(
                    "AddBoss",
                    5.5f,
                    ModContent.NPCType<CarminiteAmalgamation>(),
                    this,
                    "Carminite Amalgamation",
                    (Func<bool>)(() => DownedBossSystem.downedCarminiteAmalgamation),
                    ModContent.ItemType<SuspiciousLookingDroplet>(),
                    0,
                    new List<int> { ModContent.ItemType<CarminiteAmalgamationBag>(), ModContent.ItemType<Bloodtooth>(), ModContent.ItemType<Carminite>(), ModContent.ItemType<CarminiteBane>(), ModContent.ItemType<CarminiteRipperClaws>(), ModContent.ItemType<CarminitePurgatory>(), ModContent.ItemType<CarminiteDeadshot>(), ItemID.LesserHealingPotion },
                    "Spawn by using a [i:" + ModContent.ItemType<SuspiciousLookingDroplet>() + "]",
                    "",
                    "Eternal/Content/BossChecklist/CarminiteAmalgamation",
                    "Eternal/Content/NPCs/Boss/CarminiteAmalgamation/CarminiteAmalgamation_Head_Boss"
                );

                bossChecklist.Call(
                    "AddBoss",
                    5.9f,
                    ModContent.NPCType<DuneGolem>(),
                    this,
                    "Dune Golem",
                    (Func<bool>)(() => DownedBossSystem.downedDuneGolem),
                    ModContent.ItemType<IesniumBeacon>(),
                    0,
                    new List<int> { ItemID.LesserHealingPotion },
                    "Spawn by using an [i:" + ModContent.ItemType<IesniumBeacon>() + "] in the desert",
                    (Func<NPC, string>)((NPC) => $"The {NPC.FullName} disapears into the dunes"),
                    "Eternal/Content/BossChecklist/DuneGolem",
                    "Eternal/Content/NPCs/Boss/DuneGolem/DuneGolem_Head_Boss"
                );

                // hardmode
                bossChecklist.Call(
                    "AddBoss",
                    8.5f,
                    ModContent.NPCType<IgneopedeHead>(),
                    this,
                    "The Igneopede",
                    (Func<bool>)(() => DownedBossSystem.downedIgneopede),
                    ModContent.ItemType<MoltenBait>(),
                    0,
                    new List<int> { ItemID.GreaterHealingPotion },
                    "Spawn by using [i:" + ModContent.ItemType<MoltenBait>() + "] in the underworld",
                    (Func<NPC, string>)((NPC) => $"{NPC.FullName} burrows through the Underword"),
                    "Eternal/Content/BossChecklist/Igneopede",
                    "Eternal/Content/NPCs/Boss/Igneopede/IgneopedeHead_Head_Boss"
                );

                // post-moon lord
                bossChecklist.Call(
                    "AddBoss",
                    17.5f,
                    ModContent.NPCType<CosmicApparition>(),
                    this,
                    "Cosmic Apparition",
                    (Func<bool>)(() => DownedBossSystem.downedCosmicApparition),
                    ModContent.ItemType<OtherworldlyCosmicDebris>(),
                    0,
                    new List<int> { ModContent.ItemType<PristineHealingPotion>() },
                    "Spawn by using some [i:" + ModContent.ItemType<OtherworldlyCosmicDebris>() + "]",
                    (Func<NPC, string>)((NPC) => $"The {NPC.FullName} fades away..."),
                    "Eternal/Content/BossChecklist/CosmicApparition",
                    "Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition_Head_Boss"
                );
            }
            #endregion
        }
    }
}