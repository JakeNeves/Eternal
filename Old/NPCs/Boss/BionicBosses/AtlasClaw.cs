using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.BionicBosses
{
    public class AtlasClaw : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("AR-2006 Atlas-X9 Claw");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 12000;
            npc.width = 38;
            npc.height = 36;
            npc.damage = 40;
            npc.defense = 60;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.knockBackResist = -1f;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 24000;
            npc.damage = 60;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 36000;
                npc.damage = 80;
            }
        }

        public override void AI()
        {
            NPC parentNPC = Main.npc[(int)npc.ai[0]];
            Player player = Main.player[parentNPC.target];
            for (int i = (int)npc.ai[0]; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<Atlas>())
                {
                    parentNPC = Main.npc[i];
                    break;
                }
            }
            if (!parentNPC.active)
            {
                npc.active = false;
            }

            npc.Center = parentNPC.oldPos[4] + new Vector2(parentNPC.width / 2, parentNPC.height / 2) + new Vector2(50 * npc.ai[0], 40);
            npc.rotation = 0;
            npc.spriteDirection = (int)npc.ai[0];
        }

        public override bool CheckActive()
        {
            return false;
        }

    }
}
