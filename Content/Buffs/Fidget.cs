using Eternal.Common.GlobalNPCs;
using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class Fidget : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fidget");
            Description.SetDefault("You can't control your movement");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffSystem>().fidget = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            BuffGlobalNPC.fidget = true;
        }
    }
}
