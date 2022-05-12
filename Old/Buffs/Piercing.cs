using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs
{
    public class Piercing : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Piercing");
            Description.SetDefault("Your attacks inflict Red Fracture");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            EternalGlobalProjectile.piercingBuff = true;
            if (player.buffTime[buffIndex] == 0)
            {
                if (EternalGlobalProjectile.piercingBuff)
                {
                    EternalGlobalProjectile.piercingBuff = false;
                }
            }
        }

    }
}
