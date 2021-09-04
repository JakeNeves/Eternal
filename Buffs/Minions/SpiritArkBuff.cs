using Eternal.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs.Minions
{
    public class SpiritArkBuff : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Spirit Ark");
            Description.SetDefault("The soul-infused spirit ark will aid you in combat");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SpiritArk>()] > 0)
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
