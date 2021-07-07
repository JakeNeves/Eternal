using Eternal.Items;
using Eternal.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal
{
    public class EternalPlayer : ModPlayer
    {

        #region Defbuffs
        public bool ominousPresence = false;
        public bool doomFire = false;
        public bool embericCombustion = false;
        #endregion

        #region Minions
        public bool cEnergy = false;
        #endregion

        public bool ZoneLabrynth = false;
		public bool ZoneThunderduneBiome = false;
		public bool ZoneCommet = false;
        public bool ZoneBeneath = false;
        public bool ZoneAshpit = false;

        public override void ResetEffects()
        {
            EternalGlobalProjectile.cometGauntlet = false;
        }

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
            embericCombustion = false;
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
            else if (embericCombustion)
            {
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                player.lifeRegen -= 15;
            }
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
		{
			Item item = new Item();
			item.SetDefaults(ModContent.ItemType<StartingBag>());
            items.Add(item);
		}

        public override void UpdateBiomes()
        {
			ZoneThunderduneBiome = EternalWorld.thunderduneBiome > 100;
			ZoneCommet = EternalWorld.commet > 20;
            ZoneLabrynth = EternalWorld.labrynth > 50;
            ZoneBeneath = EternalWorld.theBeneath > 50;
            ZoneAshpit = EternalWorld.ashpit > 70;
        }
    }
}
