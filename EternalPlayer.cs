using Eternal.Items;
using Eternal.Items.Accessories;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
namespace Eternal
{
    public class EternalPlayer : ModPlayer
    {
		public bool incineriusPet = false;

		public bool ZoneThunderduneBiome = false;
		public bool ZoneCommet = false;

		public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			Item item = new Item();
			item.SetDefaults(ItemType<AncientPendant>());
			item.SetDefaults(ItemType<CreatorsMessage>());
			if (Main.expertMode)
            {
				item.SetDefaults(ItemType<BloodLocket>());
			}
			items.Add(item);
		}

        public override void UpdateBiomes()
        {
			ZoneThunderduneBiome = EternalWorld.thunderduneBiome > 100;
			ZoneCommet = EternalWorld.commet > 20;
        }
    }
}
