using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.BossBags;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Summon;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.DuneGolem;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
	public class Eternal : Mod
	{
		public const string AssetPath = $"{nameof(Eternal)}/Assets/";

		public override void Load()
        {

        }

		public override void Unload()
        {

        }

        public override void PostSetupContent()
        {
            #region Boss Checklist Integration
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
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
                    "Spawn by using the [i:" + ModContent.ItemType<SuspiciousLookingDroplet>() + "]",
                    "The Blood-Feasting Amalgamation.",
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
                    "Spawn by using the [i:" + ModContent.ItemType<IesniumBeacon>() + "] in the desert",
                    "The Possessed Desert Idol.",
                    "Eternal/Content/BossChecklist/DuneGolem",
                    "Eternal/Content/NPCs/Boss/DuneGolem/DuneGolem_Head_Boss"
                );
            }
            #endregion
        }
    }
}