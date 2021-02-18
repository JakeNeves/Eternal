using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eternal.NPCs.Boss.Empraynia
{
    [AutoloadBossHead]
    public class Empraynia : ModNPC
    {
        private int ShootTimer = 0;
        private int Phase = 0;
        private int AttackType = 0;

        private bool overhead = false;

        private float speed;

        private Player player;

        bool expert = Main.expertMode;
        bool hell = EternalWorld.hellMode;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.lifeMax = 78000;
            npc.width = 221;
            npc.height = 227;
            npc.damage = 180;
            npc.defense = 64;
            npc.knockBackResist = -1f;
            npc.boss = true;
            npc.noTileCollide = true;
            music = MusicID.Boss4;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.buffImmune[BuffID.Confused] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.HitSound = SoundID.NPCHit12;
            npc.DeathSound = SoundID.NPCDeath5;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 80000;
            npc.damage = 182;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 82000;
                npc.damage = 184;
            }
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            Vector2 targetPosition = Main.player[npc.target].position;
            Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
            npc.netAlways = true;

            float accuracy = 5f * (npc.life / npc.lifeMax);

            if (player.position.Y - 150 < npc.position.Y && npc.velocity.Y > -12)
            {
                npc.velocity.Y -= 0.2f;
                if (npc.velocity.Y > 0)
                {
                    npc.velocity.Y = 0;
                }
            }
            if (player.position.Y - 400 > npc.position.Y && npc.velocity.Y < 12)
            {
                npc.velocity.Y += 0.2f;
                if (npc.velocity.Y < 0)
                {
                    npc.velocity.Y = 0;
                }
            }
            if (player.position.Y < npc.position.Y && player.position.Y - 400 > npc.position.Y)
            {
                overhead = false;
            }
            else
            {
                overhead = true;
            }
            if (overhead)
            {
                if (player.position.X > npc.position.X && npc.velocity.X < 14)
                {
                    npc.velocity.X += 0.3f;
                }
                if (player.position.X < npc.position.X && npc.velocity.X > -14)
                {
                    npc.velocity.X -= 0.3f;
                }
                if (player.position.X < npc.position.X + 400)
                {
                    npc.velocity.X -= 0.2f;
                }
                if (player.position.X > npc.position.X - 400)
                {
                    npc.velocity.X += 0.2f;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.24f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.life = 0;
                    }
                    return;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
            lightColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Boss/Empraynia/Empraynia_Glow"));

        private bool AliveCheck(Player player)
        {
            if (player.dead)
            {
                if (npc.timeLeft > 30)
                    npc.timeLeft = 30;
                npc.velocity.Y -= 1f;
                return false;
            }
            return true;
        }

    }
}
