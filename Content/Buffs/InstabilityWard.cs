using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class InstabilityWard : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffImmune[ModContent.BuffType<RiftWithering>()] = true;
        }
    }
}
