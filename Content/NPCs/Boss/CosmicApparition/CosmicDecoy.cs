using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CosmicApparition
{
    [AutoloadBossHead]
    public class CosmicDecoy : ModNPC
    {
        public override string Texture => "Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition";

        const float Speed = 14f;
        const float Acceleration = 0.2f;
        int Timer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.TrailCacheLength[NPC.type] = 14;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("CosmicApparitionHead").Type;
                int gore2 = Mod.Find<ModGore>("CosmicApparitionBody").Type;
                int gore3 = Mod.Find<ModGore>("CosmicApparitionArm").Type;

                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
                for (int i = 0; i < 2; i++)
                    Gore.NewGore(entitySource, NPC.Center, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore3);
            }
            else
            {
                for (int k = 0; k < 10.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Shadowflame, 0, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 46;
            NPC.lifeMax = 3600;
            NPC.damage = 100;
            NPC.defense = 18;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = SoundID.NPCDeath55;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }
            Timer++;
            if (Timer >= 0)
            {
                Vector2 StartPosition = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float DirectionX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - StartPosition.X;
                float DirectionY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120 - StartPosition.Y;
                float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                float Num = Speed / Length;
                DirectionX = DirectionX * Num;
                DirectionY = DirectionY * Num;
                if (NPC.velocity.X < DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X + Acceleration;
                    if (NPC.velocity.X < 0 && DirectionX > 0)
                        NPC.velocity.X = NPC.velocity.X + Acceleration;
                }
                else if (NPC.velocity.X > DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X - Acceleration;
                    if (NPC.velocity.X > 0 && DirectionX < 0)
                        NPC.velocity.X = NPC.velocity.X - Acceleration;
                }
                if (NPC.velocity.Y < DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                    if (NPC.velocity.Y < 0 && DirectionY > 0)
                        NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                }
                else if (NPC.velocity.Y > DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                    if (NPC.velocity.Y > 0 && DirectionY < 0)
                        NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                }
                if (Main.rand.NextBool(36))
                {
                    Vector2 StartPosition2 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height / 2));
                    float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), StartPosition2.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
                    NPC.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                    NPC.netUpdate = true;
                }
            }
            NPC.rotation = NPC.velocity.X * 0.06f;
        }
    }
}