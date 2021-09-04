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
using Eternal.Items.Materials;
using Eternal.Items.Weapons.Hell;
using Eternal.Projectiles.Enemy;
using Eternal.Projectiles;

namespace Eternal.NPCs.Boss.DroxOverlord
{
    public class DroxOverlord : ModNPC
    {
        private Player player;

        #region Fundimentals
        int attackTimer;
        int Phase;
        int Timer;
        int frameNum;

        const float Speed = 12.75f;
        const float Acceleration = 0.4f;

        bool majorDamage = false;
        #endregion

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 32000;
            npc.damage = 40;
            npc.defense = 20;
            npc.knockBackResist = -1f;
            npc.width = 68;
            npc.height = 68;
            npc.value = Item.buyPrice(gold: 30);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            // bossBag = ItemType<DunekeeperBag>();
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/MechanicalEnvy");
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Electrified] = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
              // Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperEye"), 1f);
              // Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperLeftHalf"), 1f);
              // Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/DunekeeperRightHalf"), 1f);
                 CombatText.NewText(npc.Hitbox, Color.DarkRed, "CRITICAL DAMAGE TO SYSTEM DETECTED, POWERING DOWN...", dramatic: true);
            }
            else if (npc.life <= 12000)
            {
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width / 2, npc.height / 2, 6, 0f, 0f, 5, default(Color), 0.5f);
                    Main.dust[dustIndex].velocity *= 0.5f;
                }
            }
            else
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Electric, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void NPCLoot()
        {
            
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 64000;
            npc.damage = 60;
            npc.defense = 40;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 128000;
                npc.defense = 60;
                npc.damage = 80;
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

            if (Phase == 2)
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
            if (NPC.AnyNPCs(NPCType<DroxMine>()))
            {
                npc.dontTakeDamage = true;
            }
            else
            {
                npc.dontTakeDamage = false;
            }

            npc.rotation = npc.velocity.X * 0.03f;
            Vector2 direction = Main.player[npc.target].Center - npc.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            int amountOfProjectiles;
            if (Main.expertMode)
            {
                amountOfProjectiles = 8;
            }
            else if (EternalWorld.hellMode)
            {
                amountOfProjectiles = 12;
            }
            else
            {
                amountOfProjectiles = 4;
            }

            if (majorDamage)
            {
                for (int i = 0; i < 5; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 25, default(Color), 0.5f);
                    Main.dust[dustIndex].velocity *= 0.5f;
                }
            }

            #region attacks
            attackTimer++;

            if (npc.life < npc.lifeMax / 4)
            {
                Phase = 3;
                frameNum = 3;
                majorDamage = true;
            }
            else if (npc.life < npc.lifeMax / 3)
            {
                Phase = 2;
                frameNum = 2;
            }
            else if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
                frameNum = 1;
            }
            else
            {
                Phase = 0;
                frameNum = 0;
                majorDamage = false;
            }

            if(Phase == 0)
            {
                if (attackTimer == 105 || attackTimer == 110)
                {

                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        int damage = Main.expertMode ? 15 : 17;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.MartianTurretBolt, damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (EternalWorld.hellMode)
                {
                    if (attackTimer == 115 || attackTimer == 116 || attackTimer == 117 || attackTimer == 118 || attackTimer == 119 || attackTimer == 120)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 125 || attackTimer == 126 || attackTimer == 127 || attackTimer == 128 || attackTimer == 129 || attackTimer == 130)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 135 || attackTimer == 136 || attackTimer == 137 || attackTimer == 138 || attackTimer == 139 || attackTimer == 140)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 145 || attackTimer == 146 || attackTimer == 147 || attackTimer == 148 || attackTimer == 149 || attackTimer == 150)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                }
                else
                {
                    if (attackTimer == 116 || attackTimer == 118 || attackTimer == 120)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ProjectileID.EyeLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 126 || attackTimer == 128 || attackTimer == 130)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ProjectileID.EyeLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 136 || attackTimer == 138 || attackTimer == 140)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ProjectileID.EyeLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 146 || attackTimer == 148 || attackTimer == 150)
                    {
                        Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ProjectileID.EyeLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                }
                if (attackTimer == 200)
                {
                    if (EternalWorld.hellMode)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ProjectileType<Warning>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                    attackTimer = 0;
                }
            }
            if (Phase == 1)
            {
                switch (attackTimer)
                {
                    case 150:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 152:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 154:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 156:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 158:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 0, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 160:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 162:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 0, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                    case 164:
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        break;
                }
                if (attackTimer == 180 || attackTimer == 280 || attackTimer == 380 || attackTimer == 480)
                {
                    Main.PlaySound(SoundID.DD2_KoboldExplosion, npc.position);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -12, 0, ProjectileType<DroxMissile>(), 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 12, 0, ProjectileType<DroxMissile>(), 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 12, ProjectileType<DroxMissile>(), 6, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 200)
                {
                    if (EternalWorld.hellMode)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ProjectileType<Warning>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                    attackTimer = 0;
                }
            }
            if (Phase == 2)
            {
                if (EternalWorld.hellMode)
                {
                    if (attackTimer == 110 || attackTimer == 112 || attackTimer == 114 || attackTimer == 116 || attackTimer == 118 || attackTimer == 120 || attackTimer == 122 || attackTimer == 124 || attackTimer == 126)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 0, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 0, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                }
                else {
                    if (attackTimer == 110 || attackTimer == 210)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, -10, 0, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 112 || attackTimer == 212)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, 10, 0, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 114 || attackTimer == 214)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 10, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 116 || attackTimer == 216)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, -10, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 118 || attackTimer == 218)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, -8.5f, -8.5f, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 120 || attackTimer == 220)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, 8.5f, -8.5f, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 122 || attackTimer == 224)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, -8.5f, 8.5f, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                    else if (attackTimer == 124 || attackTimer == 226)
                    {
                        Projectile.NewProjectile(npc.position.X, npc.position.Y, 8.5f, 8.5f, ProjectileID.MartianWalkerLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    }
                }
                if (attackTimer == 230)
                {
                    if (EternalWorld.hellMode)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ProjectileType<Warning>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                    attackTimer = 0;
                }
            }
            if (Phase == 3)
            {
                if (attackTimer == 125 || attackTimer == 150 || attackTimer == 175 || attackTimer == 200)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 225 || attackTimer == 250 || attackTimer == 275 || attackTimer == 300)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<DroxMine>());
                }
                if (attackTimer == 325 || attackTimer == 350 || attackTimer == 375 || attackTimer == 400)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, -8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 8, ProjectileID.DeathLaser, 6, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 425 || attackTimer == 450 || attackTimer == 475 || attackTimer == 500)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<DroxMine>());
                }
                if (attackTimer == 550)
                {
                    if (EternalWorld.hellMode)
                    {
                        Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-200, 200), npc.Center.Y + Main.rand.Next(-200, 200), 0, 0, ProjectileType<Warning>(), 12, 0, Main.myPlayer, 0f, 0f);
                    }
                    attackTimer = 0;
                }
            }
            #endregion


            
        }

        

    }

        

}

