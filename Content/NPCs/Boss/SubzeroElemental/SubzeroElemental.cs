using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.SubzeroElemental
{
    [AutoloadBossHead]
    public class SubzeroElemental : ModNPC
    {

        #region Fundimentals
        private Player player;
        int AttackTimer = 0;
        int Phase = 0;
        float speed;
        #endregion

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 88;
            if (NPC.downedMoonlord)
            {
                NPC.lifeMax = 92000;
                NPC.damage = 100;
            }
            else
            {
                NPC.lifeMax = 46000;
                NPC.damage = 50;
            }
            NPC.boss = true;
            NPC.HitSound = null; //SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.aiStyle = -1;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = MusicID.Boss3;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (NPC.downedMoonlord)
            {
                NPC.lifeMax = 134000;
                NPC.damage = 200;
            }
            else
            {
                NPC.lifeMax = 92000;
                NPC.damage = 100;
            }

            if (DifficultySystem.hellMode)
            {
                if (NPC.downedMoonlord)
                {
                    NPC.lifeMax = 176000;
                    NPC.damage = 250;
                }
                else
                {
                    NPC.lifeMax = 134000;
                    NPC.damage = 200;
                }
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                
            }
            else
            {
                for (int k = 0; k < damage / NPC.lifeMax * 25; k++)
                    Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.IceTorch);
            }
        }

        public override bool PreAI()
        {
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 15), NPC.width, NPC.height - 25, DustID.Ice, NPC.oldVelocity.X * 0.25f, NPC.oldVelocity.Y * 0.25f);
            }

            return true;
        }

        public override void AI()
        {
            
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (NPC.downedMoonlord)
            {
                potionType = ItemID.SuperHealingPotion;
            }
            else
            {
                potionType = ItemID.GreaterHealingPotion;
            }

            name = "The " + name;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
