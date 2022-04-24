using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.SubzeroElemental
{
    public class SubzeroElementalShield : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Ice Barrier");
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 2000;
            npc.width = 202;
            npc.height = 202;
            npc.damage = 0;
            npc.defense = 30;
            npc.knockBackResist = -1f;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
        }

        public override void AI()
        {
            NPC parentNPC = Main.npc[(int)npc.ai[0]];
            Player player = Main.player[parentNPC.target];
            for (int i = (int)npc.ai[0]; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<SubzeroElementalShield>())
                {
                    parentNPC = Main.npc[i];
                    break;
                }
            }
            if (!parentNPC.active)
            {
                npc.active = false;
            }

            npc.Center = parentNPC.Center;
            npc.rotation += 15f;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 4000;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 6000;
            }
        }

        public override bool CheckActive()
        {
            return false;
        }
    }
}
