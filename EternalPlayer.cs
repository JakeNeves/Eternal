using Eternal.Items;
using Eternal.Items.Accessories;
using Eternal.Items.BossBags;
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

        #region Defbuffs
        public bool ominousPresence = false;
        public bool doomFire = false;
        #endregion

        #region Minions
        public bool cEnergy = false;
        #endregion

        public bool incineriusPet = false;

        public bool ZoneLabrynth = false;
		public bool ZoneThunderduneBiome = false;
		public bool ZoneCommet = false;
        public bool ZoneBeneath = false;

        public override void PostUpdate()
        {
            if (ZoneBeneath)
            {
                player.AddBuff(BuffID.Obstructed, 1);
            }
        }

        public override void UpdateDead()
        {
            doomFire = false;
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
            }
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			Item item = new Item();
			item.SetDefaults(ItemType<StartingBag>());
            items.Add(item);
		}

        public override void UpdateBiomes()
        {
			ZoneThunderduneBiome = EternalWorld.thunderduneBiome > 100;
			ZoneCommet = EternalWorld.commet > 20;
            ZoneLabrynth = EternalWorld.labrynth > 50;
            ZoneBeneath = EternalWorld.theBeneath > 50;
        }
    }
}
