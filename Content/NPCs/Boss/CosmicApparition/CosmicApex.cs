using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CosmicApparition
{
    public class CosmicApex : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.MustAlwaysDraw[NPC.type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 118;
            NPC.height = 80;
            NPC.lifeMax = 2000;
            NPC.defense = 80;
            NPC.HitSound = SoundID.DD2_SkeletonHurt;
            NPC.DeathSound = SoundID.DD2_SkeletonDeath;
            NPC.damage = 90;
            NPC.alpha = 255;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life < 0)
            {
                _ = NPC.Center;
                for (int num120 = 0; num120 < 60; num120++)
                {
                    int num121 = 25;
                    _ = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * Main.rand.Next(24, 41) / 8f;
                    int num122 = Dust.NewDust(NPC.Center - Vector2.One * num121, num121 * 2, num121 * 2, DustID.DemonTorch);
                    Dust dust154 = Main.dust[num122];
                    Vector2 vector7 = Vector2.Normalize(dust154.position - NPC.Center);
                    dust154.position = NPC.Center + vector7 * 25f * NPC.scale;
                    if (num120 < 30)
                    {
                        dust154.velocity = vector7 * dust154.velocity.Length();
                    }
                    else
                    {
                        dust154.velocity = vector7 * Main.rand.Next(45, 91) / 10f;
                    }
                    dust154.noGravity = true;
                    dust154.scale = 0.7f;
                }

                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Shadowflame, NPC.oldVelocity.X * 0.25f, NPC.oldVelocity.Y * 0.25f);
                }
            }
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            var entitySource = NPC.GetSource_FromAI();

            NPC.alpha -= 25;

            NPC.ai[2]++;
            if (NPC.ai[2] >= 3)
            {
                NPC.ai[2] = 0f;
                NPC.netUpdate = true;
            }
            Player player = Main.player[NPC.target];
            Vector2 center = NPC.Center;
            Vector2 Velocity = NPC.velocity;
            NPC.ai[1] += 1f;
            if (NPC.ai[1] >= 300f)
            {
                for (int i = 0; i < 20; i++)
                {
                    int KillDust = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DemonTorch, NPC.direction * 2, 0f, 100, default, 1.4f);
                    Dust DustExample = Main.dust[KillDust];
                    DustExample.color = Color.LightPink;
                    DustExample.color = Color.Lerp(DustExample.color, Color.White, 0.3f);
                    DustExample.noGravity = true;
                }
                NPC.life = 0;
            }
            Vector2 vectoridk = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float playerX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - vectoridk.X;
            float playerY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2 - vectoridk.Y;
            if (playerX > 0f)
            {
                NPC.spriteDirection = player.direction;
            }
            if (playerX < 0f)
            {
                NPC.spriteDirection = -player.direction;
            }
            NPC.ai[0] += 1f;
            if (NPC.ai[0] >= 15f)
            {
                NPC.ai[0] = 0f;
                NPC.netUpdate = true;

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    /*float velocityX = NPC.rotation;
                    float velocityY = NPC.rotation;
                    Vector2 vector3 = Vector2.Normalize(player.Center - center) * (NPC.width + 20) / 2f + center;
                    int bubble = NPC.NewNPC(entitySource, (int)vector3.X, (int)vector3.Y, ModContent.NPCType<DetonatingWisp>());*/

                    int wisp = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DetonatingWisp>());
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
