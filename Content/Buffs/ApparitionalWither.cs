using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class ApparitionalWither : ModBuff
    {

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;

            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffSystem>().apparitionalWither = true;
        }
    }
}
