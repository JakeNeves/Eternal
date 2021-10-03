using System.IO;
using Terraria;
using Terraria.ID;

namespace Eternal.NPCs.Miniboss.Mechworm
{
    public class MechwormHead : Mechworm
    {
        public override void SetDefaults()
        {
            Lighting.AddLight(npc.position, 2.55f, 1.23f, 0f);
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.width = 86;
            npc.height = 76;
            npc.knockBackResist = -1f;
            npc.noGravity = true;
            npc.behindTiles = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            npc.lifeMax = 25000;
            npc.damage = 50;
            npc.defense = 30;
            npc.aiStyle = -1;
        }

        public override void Init()
        {
            base.Init();
            head = true;
        }

        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }
    }
}
