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

        int phase = 0;
        int projectileShoot;
        int teleportTimer;
        int projectileTimer;

        bool canTeleport = true;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            // DisplayName.SetDefault("Wandering Soul");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
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
            NPC.width = 28;
            NPC.height = 46;
            NPC.lifeMax = 12800;
            NPC.defense = 18;
            NPC.HitSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/NPCHit/CosmicApparitionHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            //NPC.HitSound = SoundID.NPCHit52;
            NPC.DeathSound = null;
            Music = 0;
            NPC.damage = 200;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
            NPC.BossBar = Main.BigBossProgressBar.NeverValid;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (NPC.life < 0)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/CosmicApparitionAnger"));
                //SoundEngine.PlaySound(SoundID.NPCDeath10, NPC.position);
                Main.NewText("Shrieks echo as the soul angers...", 175, 75, 255);
                NPC.NewNPC(entitySource, (int)NPC.Center.X - 20, (int)NPC.Center.Y, ModContent.NPCType<CosmicApparition>());
            }
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            var entitySource = NPC.GetSource_FromAI();

            if (NPC.life < NPC.lifeMax / 2)
            {
                phase = 1;
            }

            switch (phase)
            {
                case 0:
                    #region Flying Movement P1
                    if (phase == 0)
                    {
                        float speed;
                        if (DifficultySystem.hellMode)
                        {
                            speed = 12f;
                        }
                        else
                        {
                            speed = 10f;
                        }
                        float acceleration = 0.20f;
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
                    }
                    #endregion
                    break;
                case 1:
                    #region Flying Movement P2
                    Player player = Main.player[NPC.target];
                    NPC.TargetClosest(true);
                    NPC.direction = NPC.spriteDirection = NPC.Center.X < player.Center.X ? -1 : 1;
                    Vector2 targetPosition = Main.player[NPC.target].position;
                    Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
                    NPC.netAlways = true;
                    projectileTimer++;
                    if (canTeleport)
                    {
                        teleportTimer++;
                    }
                    if (teleportTimer >= 200)
                    {
                        SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                        for (int k = 0; k < 5; k++)
                        {
                            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Shadowflame, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
                        }
                        NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                        NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);
                        teleportTimer = 0;
                    }
                    if (projectileTimer >= 180)
                    {
                        Vector2 shootPos = NPC.Center;
                        float accuracy = 5f * (NPC.life / NPC.lifeMax);
                        Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
                        shootVel.Normalize();
                        shootVel *= 14.5f;
                        projectileShoot = Main.rand.Next(0, 1);
                        for (int i = 0; i < (Main.expertMode ? 2 : 1); i++)
                        {
                            if (projectileShoot == 0)
                            {
                                Projectile.NewProjectile(entitySource, shootPos.X + -100 * NPC.direction + Main.rand.Next(-20, 20), shootPos.Y - Main.rand.Next(-20, 20), shootVel.X, shootVel.Y, ProjectileID.ShadowBeamHostile, NPC.damage / 3, 5f);
                            }
                            if (projectileShoot == 1)
                            {
                                Projectile.NewProjectile(entitySource, shootPos.X + -100 * NPC.direction + Main.rand.Next(-20, 20), shootPos.Y - Main.rand.Next(-20, 20), shootVel.X, shootVel.Y, ProjectileID.Shadowflames, NPC.damage / 2, 5f);
                            }
                        }
                        projectileTimer = 0;
                    }
                    if (!player.active || player.dead)
                    {
                        canTeleport = false;
                        NPC.TargetClosest(false);
                        NPC.direction = 1;
                        NPC.velocity.Y = NPC.velocity.Y - 0.1f;
                        if (NPC.timeLeft > 5)
                        {
                            NPC.timeLeft = 5;
                            return;
                        }
                    }
                    #endregion
                    break;
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
