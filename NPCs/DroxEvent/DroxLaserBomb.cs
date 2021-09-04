using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using System;
using Eternal.Items.Materials;

namespace Eternal.NPCs.DroxEvent
{
    public class DroxLaserBomb : ModNPC
    {

        const float Speed = 8f;
        const float Acceleration = 0.2f;
        int Timer;
        int attackTimer = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
            DisplayName.SetDefault("Drox Laser Sniper");
        }

        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 36;
            npc.aiStyle = -1;
            npc.defense = 16;
            npc.knockBackResist = -1;
            npc.lifeMax = 600;
            npc.damage = 18;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<DroxCore>());
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<DroxPlate>(), Main.rand.Next(2, 8));
        }

        public Vector2 bossCenter
        {
            get { return npc.Center; }
            set { npc.position = value - new Vector2(npc.width / 2, npc.height / 2); }
        }

        public override void AI()
        {
            attackTimer++;
            npc.rotation = npc.velocity.X * 0.03f;
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

            int amountOfProjectiles = Main.rand.Next(2, 4);
            for (int i = 0; i < amountOfProjectiles; ++i)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                int damage = Main.expertMode ? 15 : 17;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.PinkLaser, damage, 1, Main.myPlayer, 0, 0);
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
