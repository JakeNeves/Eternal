using Terraria;
using Terraria.ID;

namespace Eternal.NPCs.Miniboss.Mechworm
{
    public class MechwormBody : Mechworm
    {
        public override void SetDefaults()
        {
            Lighting.AddLight(npc.position, 2.55f, 1.23f, 0f);
            npc.CloneDefaults(NPCID.DiggerBody);
            npc.width = 58;
            npc.height = 44;
            npc.knockBackResist = -1f;
            npc.behindTiles = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.netAlways = true;
            npc.lifeMax = 25000;
            npc.damage = 50;
            npc.defense = 30;
            npc.aiStyle = -1;
            npc.boss = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 500000;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 750000;
            }
        }

        public override void Init()
        {
            base.Init();
            body = true;
        }
    }
}
