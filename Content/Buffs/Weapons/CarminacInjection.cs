using Terraria;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs.Weapons
{
    public class CarminacInjection : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense /= 2;
            player.endurance /= Main.rand.NextFloat(2f, 4f);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense /= Main.rand.Next(2, 4);
        }
    }
}
