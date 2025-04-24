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
            player.statDefense += 24;
            player.endurance += 0.9f;
            player.moveSpeed += 1.05f;
        }
    }
}
