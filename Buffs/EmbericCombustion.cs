using Eternal.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Buffs
{
    public class EmbericCombustion : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Emberic Combustion");
            Description.SetDefault("You are burning...");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EternalPlayer>().embericCombustion = true;
            if (player.buffTime[buffIndex] == 0)
            {
                if (player.GetModPlayer<EternalPlayer>().embericCombustion)
                {
                    player.GetModPlayer<EternalPlayer>().embericCombustion = false;
                }
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EternalGlobalNPC>().embericComustion = true;
        }

    }
}
