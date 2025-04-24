using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Weapons
{
    public class ImmenseArmorFracture : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense /= 3;
            player.endurance /= 3;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense /= 3;
        }
    }
}
