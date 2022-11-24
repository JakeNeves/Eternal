using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

			// pre-hardmode bosses
			#region Carminite Amalgamation
			string CAName = "The Carminite Amalgamation";

			int CAType = ModContent.NPCType<Content.NPCs.Boss.CarminiteAmalgamation.CarminiteAmalgamation>();

		    float CAweight = 5.5f;

			Func<bool> CAdowned = () => DownedBossSystem.downedCarminiteAmalgamation;

			Func<bool> CAavailable = () => true;

			List<int> CAcollection = new List<int>()
			{
				ModContent.ItemType<Content.Items.Materials.Carminite>(),
				ModContent.ItemType<Content.Items.Weapons.Melee.CarminiteBane>(),
				ModContent.ItemType<Content.Items.Weapons.Melee.CarminiteRipperClaws>(),
				ModContent.ItemType<Content.Items.Weapons.Melee.CarminitePurgatory>(),
				ModContent.ItemType<Content.Items.Weapons.Ranged.CarminiteDeadshot>()
			};

			int CAsummonItem = ModContent.ItemType<Content.Items.Summon.SuspiciousLookingDroplet>();

			string CAspawnInfo = $"Spawn by using a [i:{CAsummonItem}]";

			string CAdespawnInfo = null;
			#endregion
			#region Dune Golem
			string DGName = "The Dune Golem";

			int DGType = ModContent.NPCType<Content.NPCs.Boss.DuneGolem.DuneGolem>();

			float DGweight = 5.9f;

			Func<bool> DGdowned = () => DownedBossSystem.downedDuneGolem;

			Func<bool> DGavailable = () => true;

			List<int> DGcollection = new List<int>()
			{
				ModContent.ItemType<Content.Items.Materials.MalachiteShard>()
			};

			int DGsummonItem = ModContent.ItemType<Content.Items.Summon.IesniumBeacon>();

			string DGspawnInfo = $"Spawn by using a [i:{DGsummonItem}]";

			string DGdespawnInfo = "The Dune Golem disapears into the dunes...";
			#endregion

			// hardmode bosses
			#region Igneopede
			#endregion

			// post-moon lord bosses
			#region Cosmic Apparition
			string CosAppName = "The Cosmic Apparition";

			int CosAppType = ModContent.NPCType<Content.NPCs.Boss.CosmicApparition.CosmicApparition>();

			float CosAppweight = 19.5f;

			Func<bool> CosAppdowned = () => DownedBossSystem.downedCosmicApparition;

			Func<bool> CosAppavailable = () => true;

			List<int> CosAppcollection = new List<int>()
			{
				ModContent.ItemType<Content.Items.Materials.ApparitionalMatter>(),
				ModContent.ItemType<Content.Items.Materials.Astragel>(),
				ModContent.ItemType<Content.Items.Materials.InterstellarSingularity>(),
				ModContent.ItemType<Content.Items.Materials.StarmetalBar>(),
				ModContent.ItemType<Content.Items.Weapons.Melee.Vexation>(),
				ModContent.ItemType<Content.Items.Weapons.Melee.ApparitionalDisk>(),
				ModContent.ItemType<Content.Items.Weapons.Ranged.Starfall>(),
				ModContent.ItemType<Content.Items.Pets.ReminantHead>()
			};

			int CosAppsummonItem = ModContent.ItemType<Content.Items.Summon.OtherworldlyCosmicDebris>();

			string CosAppspawnInfo = $"Spawn by using [i:{CosAppsummonItem}]";

			string CosAppdespawnInfo = "The Cosmic Apparition fades away...";
			#endregion
			#region Ark of Imperious
			string AOIName = "The Ark of Imperious";

			int AOIType = ModContent.NPCType<Content.NPCs.Boss.AoI.ArkofImperious>();

			float AOIweight = 20f;

			Func<bool> AOIdowned = () => DownedBossSystem.downedArkofImperious;

			Func<bool> AOIavailable = () => true;

			List<int> AOIcollection = new List<int>()
			{
				ModContent.ItemType<Content.Items.Materials.RawArkrystal>(),
				ModContent.ItemType<Content.Items.Materials.RawArkaniumDebris>(),
				ModContent.ItemType<Content.Items.Materials.WeatheredPlating>(),
				ModContent.ItemType<Content.Items.Materials.UnrefinedHeroSword>(),
			};

			int AOIsummonItem = ModContent.ItemType<Content.Items.Summon.RoyalShrineSword>();

			string AOIspawnInfo = $"Spawn by using a [i:{AOIsummonItem}]";

			string AOIdespawnInfo = "The Ark of Imperious returns to his domain...";
			#endregion
			#region The Cosmic Emperor
			string CEName = "The Cosmic Emperor";

			int CEType = ModContent.NPCType<Content.NPCs.Boss.CosmicEmperor.CosmicEmperorThrone>();

			float CEweight = 32.5f;

			Func<bool> CEdowned = () => DownedBossSystem.downedCosmicEmperor;

			Func<bool> CEavailable = () => true;

			List<int> CEcollection = new List<int>()
			{

			};

			int CEsummonItem = ModContent.ItemType<Content.Items.Summon.CosmicTablet>();

			string CEspawnInfo = $"Spawn by using the [i:{CEsummonItem}] at the Altar of Cosmic Desire";

			string CEdespawnInfo = null;
			#endregion

			// pre hardmode
			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				// Carminite Amalgamation
				CAName,
				CAType,
				CAweight,
				CAdowned,
				CAavailable,
				CAcollection,
				CAsummonItem,
				CAspawnInfo,
				CAdespawnInfo
			);

			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				// Dune Golem
				DGName,
				DGType,
				DGweight,
				DGdowned,
				DGavailable,
				DGcollection,
				DGsummonItem,
				DGspawnInfo,
				DGdespawnInfo
			);

			// hardmode
			// Igneopede
			// Incinerius
			// Subzero Elemental
			// Duneworm
			// Empraynia
			// Armageddon Golem
			// Armageddon Elemental

			// post-moon lord
			// Frost King
			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				// Cosmic Apparition
				CosAppName,
				CosAppType,
				CosAppweight,
				CosAppdowned,
				CosAppavailable,
				CosAppcollection,
				CosAppsummonItem,
				CosAppspawnInfo,
				CosAppdespawnInfo
			);

			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				// Ark of Imperious
				AOIName,
				AOIType,
				AOIweight,
				AOIdowned,
				AOIavailable,
				AOIcollection,
				AOIsummonItem,
				AOIspawnInfo,
				AOIdespawnInfo
			);

			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				// Cosmic Emperor
				CEName,
				CEType,
				CEweight,
				CEdowned,
				CEavailable,
				CEcollection,
				CEsummonItem,
				CEspawnInfo,
				CEdespawnInfo
			);
		}
	}
}
