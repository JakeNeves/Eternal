using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Weapons
{
    public class BloodyMurder : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Ranged) *= 2;
        }
    }
}
