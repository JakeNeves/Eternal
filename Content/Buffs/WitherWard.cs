using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class WitherWard : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[ModContent.BuffType<ApparitionalWither>()] = true;
        }
    }
}
