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
    }
}