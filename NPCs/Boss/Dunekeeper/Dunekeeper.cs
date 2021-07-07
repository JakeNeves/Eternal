using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.BossBags;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Items.Armor;
using Eternal.Projectiles.Boss;

namespace Eternal.NPCs.Boss.Dunekeeper
{
    [AutoloadBossHead]
    public class Dunekeeper : ModNPC
    {
        private Player player;

        #region Fundimentals
        int attackTimer;
        int Phase;
        int Timer;
        int frameNum;

        const float Speed = 14f;
        const float Acceleration = 0.2f;
        #endregion

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 9000;
            npc.damage = 5;
            npc.defense = 15;
            npc.knockBackResist = 0f;
            npc.width = 46;
            npc.height = 46;
            npc.value = Item.buyPrice(gold: 30);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.buffImmune[BuffID.Electrified] = true;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            bossBag = ItemType<DunekeeperBag>();
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DunesWrath");
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperEye"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperLeftHalf"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperRightHalf"), 1f);
            }
            else
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Tungsten, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }
            }
        }

        public override void NPCLoot()
        {
            
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/AltBossDefeat"), Main.myPlayer);

            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
            Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<ThunderduneHeadgear>());
                }
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<Wasteland>());
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemType<StormBeholder>());
                }
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Boss/Dunekeeper/Dunekeeper_Glow"));

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 12000;
            npc.damage = (int)(npc.damage + 1f);
            npc.defense = (int)(npc.defense + numPlayers);
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 24000;
            }
        }

        public override bool PreAI()
        {
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }

            if (Phase == 1)
            {
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
                        float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0f)), StartPosition2.X - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
                        npc.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                        npc.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                        npc.netUpdate = true;
                    }
                }
            }
            else {
                float speed;
                if (EternalWorld.hellMode)
                {
                    speed = 10f;
                }
                else
                {
                    speed = 8f;
                }
                float acceleration = 0.10f;
                Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
                float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
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
                if (npc.velocity.X < xDir)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                    if (npc.velocity.X < 0 && xDir > 0)
                        npc.velocity.X = npc.velocity.X + acceleration;
                }
                else if (npc.velocity.X > xDir)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.X > 0 && xDir < 0)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                if (npc.velocity.Y < yDir)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                    if (npc.velocity.Y < 0 && yDir > 0)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                }
                else if (npc.velocity.Y > yDir)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (npc.velocity.Y > 0 && yDir < 0)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                }
            }

            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameNum * frameHeight;
        }

        public override void AI()
        {
            Vector2 direction = Main.player[npc.target].Center - npc.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            int amountOfProjectiles;
            if (Phase == 1)
            {
                amountOfProjectiles = 4;
            }
            else
            {
                amountOfProjectiles = 2;
            }

            #region attacks
            attackTimer++;

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
                frameNum = 1;
                npc.rotation += npc.velocity.X * 0.1f;
            }
            else
            {
                npc.rotation = npc.velocity.X * 0.03f;
            }

            if (Phase == 1)
            {
                if (attackTimer == 100 || attackTimer == 150)
                {
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        int damage = Main.expertMode ? 15 : 17;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileType<DunekeeperHand>(), damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 200 || attackTimer == 250)
                {
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 0, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 0, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 300)
                {
                    attackTimer = 0;
                }
            }
            else
            {
                if (attackTimer == 100 || attackTimer == 150)
                {
                    
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        int damage = Main.expertMode ? 15 : 17;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileType<DunekeeperHand>(), damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 200 || attackTimer == 250)
                {
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.MartianTurretBolt, 6, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 300)
                {
                    attackTimer = 0;
                }
            }
            #endregion


            
        }

        

    }

        

}

