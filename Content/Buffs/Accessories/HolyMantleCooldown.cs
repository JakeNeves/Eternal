using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Accessories
{
    public class HolyMantleCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;

            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffSystem>().holyMantle = false;
        }
    }
}
