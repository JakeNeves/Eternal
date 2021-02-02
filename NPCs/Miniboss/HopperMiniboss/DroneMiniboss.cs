using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Eternal.Items;
using static Terraria.ModLoader.ModContent;
using System;

namespace Eternal.NPCs.Miniboss.HopperMiniboss
{
    public class DroneMiniboss : ModNPC
    {

        const float Speed = 8f;
        const float Acceleration = 0.2f;
        int Timer;
        int attackTimer = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
            DisplayName.SetDefault("A Scouter Drone");
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 46;
            npc.aiStyle = -1;
            npc.defense = 16;
            npc.knockBackResist = -1;
            npc.lifeMax = 16000;
            npc.damage = 18;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/MinorAttack");
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

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 32000;
            npc.damage = 20;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 64000;
                npc.damage = 22;
                npc.defense = 20;
            }
        }

        public Vector2 bossCenter
        {
            get { return npc.Center; }
            set { npc.position = value - new Vector2(npc.width / 2, npc.height / 2); }
        }

        public override void AI()
        {
            attackTimer++;
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
            switch (attackTimer)
            {
                case 10:
                    Shoot(player);
                    break;
                case 20:
                    Shoot(player);
                    break;
                case 30:
                    Shoot(player);
                    break;
                case 110:
                    Shoot(player);
                    break;
                case 120:
                    Shoot(player);
                    break;
                case 130:
                    Shoot(player);
                    break;
                case 200:
                    attackTimer = 0;
                    break;
            }
            #endregion
        }

        private void Shoot(Player player)
        {
            float angle = Main.rand.Next(0, (int)Math.PI * 200) / 100f;
            Vector2 vel = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 3 * Main.rand.Next(5);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y - 15, vel.X + 15, vel.Y, ProjectileID.BulletDeadeye, 30, 5f);
            Projectile.NewProjectile(bossCenter.X, bossCenter.Y + 15, vel.X - 15, vel.Y, ProjectileID.BulletDeadeye, 30, 5f);
            Main.PlaySound(SoundID.Item11, npc.position);
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }
    }
}
