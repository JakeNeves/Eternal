using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Graphics.Effects;
using Eternal.Projectiles.Enemy;
using Eternal.Items.BossBags;
using Eternal.Items.Weapons.Magic;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Dusts;
using Eternal.Items.Weapons.Throwing;
using Eternal.Projectiles.Boss;

namespace Eternal.NPCs.Boss.CosmicApparition
{
    [AutoloadBossHead]
    public class CosmicApparition : ModNPC
    {
        public int frameNum;
        int teleportTimer;
        int attackTimer;
        int generalAttackTimer;
        int moveTimer;

        int phase = 0;

        const float acceleration = 0.2f;
        const float speed = 14f;

        bool teleport = true;
        bool expert = Main.expertMode;

        public static int cAppGlobalFrame;

        private Player player;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 64;
            npc.height = 50;
            npc.lifeMax = 256000;
            npc.damage = 100;
            npc.defense = 20;
            npc.knockBackResist = -1f;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ApparitionalAccumulation");
            npc.HitSound = SoundID.NPCHit52;
            npc.DeathSound = SoundID.NPCDeath55;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            bossBag = ModContent.ItemType<CosmicApparitionBag>();
        }

        public override void AI()
        {
            Vector2 targetPosition = Main.player[npc.target].position;

            if(frameNum == 0)
            {
                cAppGlobalFrame = 0;
            } 
            else if (frameNum == 1)
            {
                cAppGlobalFrame = 1;
            } 
            else if (frameNum == 2)
            {
                cAppGlobalFrame = 2;
            }

            if (npc.life <= npc.lifeMax / 2)
            {
                frameNum = 1;
                phase = 1;
                attackTimer++;
            }

            if (npc.life <= npc.lifeMax / 3)
            {
                frameNum = 2;
            }

            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Starmetal>(), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
            }

            #region Flying
            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }
            moveTimer++;
            if (moveTimer >= 0)
            {
                Vector2 StartPosition = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float DirectionX = Main.player[npc.target].position.X + Main.player[npc.target].width / 2 - StartPosition.X;
                float DirectionY = Main.player[npc.target].position.Y + Main.player[npc.target].height / 2 - StartPosition.Y;
                float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                float Num = speed / Length;
                DirectionX = DirectionX * Num;
                DirectionY = DirectionY * Num;
                if (npc.velocity.X < DirectionX)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                    if (npc.velocity.X < 0 && DirectionX > 0)
                        npc.velocity.X = npc.velocity.X + acceleration;
                }
                else if (npc.velocity.X > DirectionX)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.X > 0 && DirectionX < 0)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                if (npc.velocity.Y < DirectionY)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                    if (npc.velocity.Y < 0 && DirectionY > 0)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                }
                else if (npc.velocity.Y > DirectionY)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (npc.velocity.Y > 0 && DirectionY < 0)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
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

            player.AddBuff(BuffID.Horrified, 1, false);

            
            if (attackTimer == 100)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()))
                {
                    NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<CosmicDecoy>());
                }

            }
            else if(attackTimer == 200 || attackTimer == 225 || attackTimer == 250 || attackTimer == 275)
            {
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ModContent.ProjectileType<CosmicPierce>(), 6, 0, Main.myPlayer, 0f, 0f);
            }
            if (attackTimer == 300)
            {
                attackTimer = 0;
            }

            generalAttackTimer++;

            if(generalAttackTimer == 100 || generalAttackTimer == 125 || generalAttackTimer == 150 || generalAttackTimer == 175)
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;

                int amountOfProjectiles = Main.rand.Next(4, 16);
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    int damage = Main.expertMode ? 15 : 17;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CosmicPierce>(), damage, 1, Main.myPlayer, 0, 0);
                }
            }
            else if(generalAttackTimer == 200)
            {
                generalAttackTimer = 0;
            }

            if (phase == 1)
            {
                if (NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()))
                {
                    npc.dontTakeDamage = true;
                }
                if (!NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()))
                {
                    npc.dontTakeDamage = false;
                }
            }

            if (teleport)
            {
                teleportTimer++;
            }
            if (teleportTimer == 250)
            {
                Main.PlaySound(SoundID.DD2_DarkMageCastHeal, Main.myPlayer);
                npc.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, DustID.Shadowflame, npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
                }
                teleportTimer = 0;

                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ModContent.ProjectileType<ApparitionalDiskHostile>(), 6, 0, Main.myPlayer, 0f, 0f);

            }

            if(!player.active || player.dead)
            {
                teleport = false;
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.velocity.Y = npc.velocity.Y - 0.1f;
                SkyManager.Instance.Deactivate("Eternal:Empraynia");
                if (npc.timeLeft > 5)
                {
                    npc.timeLeft = 5;
                    return;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameNum * frameHeight;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < damage / npc.lifeMax * 50; k++)
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void NPCLoot()
        {
            Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                int damage = expert ? 15 : 19;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 14f, direction.Y * 14f, ProjectileID.DD2DrakinShot, damage, 1, Main.myPlayer, 0, 0);
            }

            Main.NewText("The sky resumes back to it's tranquil state...", 220, 0, 210);
            SkyManager.Instance.Deactivate("Eternal:Empraynia");

            if (!EternalWorld.downedCosmicApparition)
            {
                Main.NewText("The cosmic entities have been empowered...", 240, 0, 240);
                EternalWorld.downedCosmicApparition = true;
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Cometstorm>());
                }
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ApparitionalDisk>());
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CosmicFist>());
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ApparitionalRendingStaff>());
                }

            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
