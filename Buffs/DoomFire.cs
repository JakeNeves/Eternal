using Eternal.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Buffs
{
    public class DoomFire : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Doom Fire");
            Description.SetDefault("Incinerius's Wrath Consumes Your Soul...");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EternalPlayer>().doomFire = true;
            if (player.buffTime[buffIndex] == 0)
            {
                if (player.GetModPlayer<EternalPlayer>().doomFire)
                {
                    player.GetModPlayer<EternalPlayer>().doomFire = false;
                }
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EternalGlobalNPC>().doomFire = true;
        }

    }
}
