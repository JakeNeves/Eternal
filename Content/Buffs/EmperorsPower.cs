using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class EmperorsPower : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 12;
            player.endurance += 0.3f;
            player.moveSpeed += 1.05f;
        }
    }
}
