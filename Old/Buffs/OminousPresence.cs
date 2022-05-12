using Eternal.NPCs.Boss.AoI;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Buffs
{
    public class OminousPresence : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ominous Presence");
            Description.SetDefault("You're Being Watched by an unknown presence...");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 8;

            if (player.buffTime[buffIndex] == 100 && !NPC.AnyNPCs(NPCType<ArkofImperious>()))
            {
                NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.AoI.ArkofImperious>());
            }
        }

    }
}
