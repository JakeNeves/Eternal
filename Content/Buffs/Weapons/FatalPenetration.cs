using Eternal.Common.Configurations;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Weapons
{
    public class FatalPenetration : ModBuff
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ServerConfig.instance.update14;
        }

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense /= 4;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense /= 4;
        }
    }
}
