using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Armor
{
    public class BlightPower : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 6;
            player.GetDamage(DamageClass.Generic) *= 0.1f;
            player.GetAttackSpeed(DamageClass.Generic) *= 0.05f;
        }
    }
}
