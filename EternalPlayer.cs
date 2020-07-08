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
    class EternalPlayer : ModPlayer
    {
		public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			Item item = new Item();
			item.SetDefaults(ItemType<AncientPendant>());
			item.SetDefaults(ItemType<CreatorsMessage>());
			items.Add(item);
		}
		
		public override void UpdateBiomes()
		{
			ZoneThunderduneBiome = (EternalWorld.thunderduneBiome > 0);
		}
		
	}
}
