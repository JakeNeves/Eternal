using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Eternal.NPCs.Miniboss.HopperMiniboss
{
    public class HopperMiniboss : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 6;
            DisplayName.SetDefault("A Scouter Drone");
        }

        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 40;
            npc.aiStyle = 1;
            npc.defense = 16;
            npc.knockBackResist = -1;
            npc.lifeMax = 32000;
            npc.damage = 10;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 64000;
            npc.damage = 12;
            
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 12800;
                npc.damage = 14;
                npc.defense = 20;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.DarkRed, "DAMAGE IS CRITICAL, DEPLOYING DRONE.", dramatic: true);
                NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, NPCType<DroneMiniboss>());
            }
            else
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Electric);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

    }
}
