using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Weapons
{
    public class MrFishbonesBoon : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Melee) *= 4;
            player.moveSpeed += 0.5f;
            player.endurance += 0.8f;
        }
    }
}
