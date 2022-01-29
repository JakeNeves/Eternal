using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs
{
    public class RedFracture : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Red Fracture");
            Description.SetDefault("Heavily Decreased Defense");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 25;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 25;
        }

    }
}
