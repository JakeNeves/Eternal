using Eternal.Common.Configurations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Weapons
{
    public class BloodyMurder : ModBuff
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Ranged) *= 2;
        }
    }
}
