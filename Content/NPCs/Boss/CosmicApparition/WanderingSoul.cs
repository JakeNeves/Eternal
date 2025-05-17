using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CosmicApparition
{
    [AutoloadBossHead]
    public class WanderingSoul : ModNPC
    {
        public override string Texture => "Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition";
        public override string BossHeadTexture => "Eternal/Content/NPCs/Boss/CosmicApparition/CosmicApparition_Head_Boss";

        int phase = 0;
        int projectileShoot;
        int teleportTimer;
        int projectileTimer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 56;
            NPC.lifeMax = 12800;
            NPC.defense = 18;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/CosmicApparitionHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            NPC.DeathSound = null;
            if (!Main.dedServ)
                Music = 0;
            NPC.damage = 200;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (NPC.life < 0)
            {
                if (!Main.dedServ)
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/CosmicApparitionAnger"), NPC.position);

                Main.NewText("Shrieks echo as the soul angers...", 175, 75, 255);
                NPC.NewNPC(entitySource, (int)NPC.Center.X - 20, (int)NPC.Center.Y, ModContent.NPCType<CosmicApparition>());
            }
        }
        public override void AI()
        {
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);
            NPC.rotation = NPC.velocity.X * 0.03f;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 playerPosition = Main.player[NPC.target].position;
            NPC.spriteDirection = NPC.direction = NPC.Center.X < player.Center.X ? -1 : 1;
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }

            float speed = 8f;
            float acceleration = 0.10f;
            Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
            if (length > 400 && Main.expertMode)
            {
                ++speed;
                acceleration += 0.05F;
                if (length > 600)
                {
                    ++speed;
                    acceleration += 0.05F;
                    if (length > 800)
                    {
                        ++speed;
                        acceleration += 0.05F;
                    }
                }
            }
            float num10 = speed / length;
            xDir = xDir * num10;
            yDir = yDir * num10;
            if (NPC.velocity.X < xDir)
            {
                NPC.velocity.X = NPC.velocity.X + acceleration;
                if (NPC.velocity.X < 0 && xDir > 0)
                    NPC.velocity.X = NPC.velocity.X + acceleration;
            }
            else if (NPC.velocity.X > xDir)
            {
                NPC.velocity.X = NPC.velocity.X - acceleration;
                if (NPC.velocity.X > 0 && xDir < 0)
                    NPC.velocity.X = NPC.velocity.X - acceleration;
            }
            if (NPC.velocity.Y < yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y + acceleration;
                if (NPC.velocity.Y < 0 && yDir > 0)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
            }
            else if (NPC.velocity.Y > yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y - acceleration;
                if (NPC.velocity.Y > 0 && yDir < 0)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
            }

            if (NPC.life < NPC.lifeMax / 2)
            {
                teleportTimer++;
                if (teleportTimer == 450)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    NPC.position.X = playerPosition.X + Main.rand.Next(-600, 600);
                    NPC.position.Y = playerPosition.Y + Main.rand.Next(-600, 600);
                    teleportTimer = 0;
                }
            }

            if (!player.active || player.dead)
            {
                NPC.velocity.Y -= 0.04f;
                NPC.EncourageDespawn(10);
                return;
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
