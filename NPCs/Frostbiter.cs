using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Eternal.Items.Placeable;
using Eternal.Projectiles.Enemy;
using System.Linq;
using Eternal.Items.Materials.Elementalblights;

namespace Eternal.NPCs
{
    public class Frostbiter : ModNPC
    {

        const float Speed = 8.75f;
        const float Acceleration = 0.4f;
        int Timer;
        int attackTimer = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 69;
            npc.aiStyle = -1;
            npc.defense = 16;
            npc.knockBackResist = -1;
            npc.lifeMax = 1200;
            npc.damage = 18;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void NPCLoot()
        {
            if (EternalWorld.downedSubzeroElementalP2)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SydaniteOre>(), Main.rand.Next(6, 36));
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrostblightCrystal>(), Main.rand.Next(4, 12));
            }
        }

        public override void AI()
        {
            attackTimer++;
            npc.rotation = npc.velocity.ToRotation() + MathHelper.ToRadians(90f);
            #region movement
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
            #endregion

            #region attacks
            if (attackTimer == 25 || attackTimer == 35 || attackTimer == 45 || attackTimer == 55)
            {
                Shoot(player);
            }
            else if (attackTimer == 125 || attackTimer == 135 || attackTimer == 145 || attackTimer == 155)
            {
                Shoot(player);
            }
            else if (attackTimer == 160)
            {
                attackTimer = 0;
            }
            #endregion
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && player.ZoneSnow && NPC.downedMoonlord ? 2.09f : 0f;
            }
            return SpawnCondition.OverworldNight.Chance * 0.5f;
        }

        private void Shoot(Player player)
        {
            /*float angle = Main.rand.Next(0, (int)Math.PI * 200) / 100f;
            Vector2 vel = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 3 * Main.rand.Next(5);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 15, vel.X + 15, vel.Y, ProjectileID.BulletDeadeye, 30, 5f);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 15, vel.X - 15, vel.Y, ProjectileID.BulletDeadeye, 30, 5f);*/

            Vector2 direction = Main.player[npc.target].Center - npc.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            int amountOfProjectiles = Main.rand.Next(1, 2);
            for (int i = 0; i < amountOfProjectiles; ++i)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                int damage = Main.expertMode ? 15 : 17;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<FridgedShot>(), damage, 1, Main.myPlayer, 0, 0);
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.5f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
    }
}
