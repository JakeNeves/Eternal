using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class Error : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += Main.rand.Next(0, 999);
            player.endurance += Main.rand.NextFloat(0f, 999f);
            player.moveSpeed += Main.rand.NextFloat(0f, 9.99f);
            player.statLife = Main.rand.Next(1, player.statLifeMax2);

            player.GetModPlayer<BuffSystem>().error = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense = Main.rand.Next(0, 999);
            npc.life = Main.rand.Next(1, npc.lifeMax);
            npc.damage = Main.rand.Next(0, 999);
        }
    }
}
