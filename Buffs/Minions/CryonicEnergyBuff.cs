using Eternal.Projectiles.Minions;
using System;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Buffs.Minions
{
    public class CryonicEnergyBuff : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cryonic Energy");
            Description.SetDefault("The Cryonic Energy made from lose shards of ice will aid you in combat");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            EternalPlayer modPlayer = (EternalPlayer)player.GetModPlayer(mod, "EternalPlayer");
            if (player.ownedProjectileCounts[ProjectileType<CryonicEnergy>()] > 0)
            {
                modPlayer.cEnergy = true;
            }
            if (!modPlayer.cEnergy)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }

    }
}
