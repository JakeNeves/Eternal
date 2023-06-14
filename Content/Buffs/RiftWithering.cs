using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class RiftWithering : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense /= 2;
            player.endurance /= 2f;
        }
    }
}
