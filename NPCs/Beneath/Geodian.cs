using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using Eternal.Tiles;
using System.Linq;
using System;

namespace Eternal.NPCs.Beneath
{
    public class Geodian : ModNPC
    {
        const float Speed = 10.5f;
        const float Acceleration = 0.2f;

        int moveTimer;
        int Timer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 9;
        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.knockBackResist = -1f;
            npc.height = 52;
            npc.lifeMax = 480;
            npc.defense = 6;
            npc.damage = 10;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit41;
            npc.DeathSound = SoundID.NPCDeath44;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Stone);
        }

        #region settings
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.12f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
            
        #endregion

        public override void AI()
        {
            moveTimer++;

            switch (moveTimer)
            {
                case 100:
                    npc.TargetClosest(true);
                    npc.spriteDirection = npc.direction;
                    Player player = Main.player[npc.target];
                    if (player.dead || !player.active)
                    {
                        npc.TargetClosest(false);
                        npc.active = false;
                    }
                    Timer++;
                    if (Timer >= 0)
                    {
                        Vector2 StartPosition = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float DirectionX = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - StartPosition.X;
                        float DirectionY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120 - StartPosition.Y;
                        float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                        float Num = Speed / Length;
                        DirectionX = DirectionX * Num;
                        DirectionY = DirectionY * Num;
                        if (npc.velocity.X < DirectionX)
                        {
                            npc.velocity.X = npc.velocity.X + Acceleration;
                            if (npc.velocity.X < 0 && DirectionX > 0)
                                npc.velocity.X = npc.velocity.X + Acceleration;
                        }
                        else if (npc.velocity.X > DirectionX)
                        {
                            npc.velocity.X = npc.velocity.X - Acceleration;
                            if (npc.velocity.X > 0 && DirectionX < 0)
                                npc.velocity.X = npc.velocity.X - Acceleration;
                        }
                        if (npc.velocity.Y < DirectionY)
                        {
                            npc.velocity.Y = npc.velocity.Y + Acceleration;
                            if (npc.velocity.Y < 0 && DirectionY > 0)
                                npc.velocity.Y = npc.velocity.Y + Acceleration;
                        }
                        else if (npc.velocity.Y > DirectionY)
                        {
                            npc.velocity.Y = npc.velocity.Y - Acceleration;
                            if (npc.velocity.Y > 0 && DirectionY < 0)
                                npc.velocity.Y = npc.velocity.Y - Acceleration;
                        }
                        if (Main.rand.Next(36) == 1)
                        {
                            Vector2 StartPosition2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height / 2));
                            float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), StartPosition2.X - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                            npc.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                            npc.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                            npc.netUpdate = true;
                        }
                    }
                    break;
                case 150:
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 0, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 0, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    break;
                case 200:
                    moveTimer = 0;
                    break;
            }
            
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { ModContent.TileType<Grimstone>(), TileID.Dirt, TileID.Stone };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneBeneath && NPC.downedBoss3 ? 2.09f : 0f;
            }
            return SpawnCondition.OverworldNightMonster.Chance * 0.5f;
        }
    }
}
