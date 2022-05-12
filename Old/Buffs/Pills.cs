using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs
{
    public class Pills : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("I Found Pills");
            Description.SetDefault("And ate them...");
            //Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 6;
            player.allDamage += 0.8f;
        }

    }
}
