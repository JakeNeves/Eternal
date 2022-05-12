using Eternal.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CryonicEnergy>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

    }
}
