using Eternal.Items.BossBags;
using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Eternal.Items.Weapons.Magic;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.Weapons.Throwing;
using Eternal.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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

        public static int cAppAnimNumber;

        private Player player;

        bool phase2Warn = false;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 4;
            NPCID.Sets.TrailCacheLength[npc.type] = 14;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 46;
            npc.lifeMax = 120000;
            npc.damage = 100;
            npc.defense = 60;
            npc.knockBackResist = -1f;
            npc.boss = true;
            music = MusicID.Boss2;
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

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 240000;
            npc.defense = 75;
            npc.damage = 110;
            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 360000;
                npc.defense = 80;
                npc.damage = 120;
            }
        }

        public override void AI()
        {
            Lighting.AddLight(npc.position, 0.75f, 0f, 0.75f);

            Vector2 targetPosition = Main.player[npc.target].position;

            if (npc.life <= npc.lifeMax / 2)
            {
                phase = 1;
                attackTimer++;
            }

            //for (int k = 0; k < 5; k++)
            //{
            //    Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, ModContent.DustType<Starmetal>(), npc.oldVelocity.X * 0.5f, npc.oldVelocity.Y * 0.5f);
            //}

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
            npc.rotation = npc.velocity.X * 0.06f;
            #endregion

            player.AddBuff(BuffID.Horrified, 1, false);


            if (attackTimer == 100)
            {
                if (!NPC.AnyNPCs(ModContent.NPCType<CosmicDecoy>()))
                {
                    int amountOfClones = Main.rand.Next(1, 3);
                    for (int i = 0; i < amountOfClones; ++i)
                    {
                        NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-30, 30), (int)npc.Center.Y + Main.rand.Next(-30, 30), ModContent.NPCType<CosmicDecoy>());
                    }
                }

            }
            else if (attackTimer == 200 || attackTimer == 225 || attackTimer == 250 || attackTimer == 275)
            {
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ModContent.ProjectileType<CosmicPierce>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
            }
            if (attackTimer == 300)
            {
                attackTimer = 0;
            }

            generalAttackTimer++;

            if (generalAttackTimer == 100 || generalAttackTimer == 125 || generalAttackTimer == 150 || generalAttackTimer == 175)
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
            else if (generalAttackTimer == 200)
            {
                generalAttackTimer = 0;
            }

            if (phase == 1)
            {
                if (EternalWorld.hellMode)
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

                if (!phase2Warn)
                {
                    Main.PlaySound(SoundID.NPCDeath10, (int)npc.position.X, (int)npc.position.Y);
                    phase2Warn = true;
                }

                if (EternalWorld.hellMode)
                {
                    player.AddBuff(BuffID.Obstructed, 1, false);
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

            if (!player.active || player.dead)
            {
                teleport = false;
                npc.TargetClosest(false);
                npc.direction = 1;
                npc.velocity.Y = npc.velocity.Y - 0.1f;
                if (npc.timeLeft > 5)
                {
                    npc.timeLeft = 5;
                    return;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            /*if (npc.life < npc.lifeMax / 2)
            {
                if (++npc.frameCounter > 8)
                {
                    npc.frameCounter = 5;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y >= 8 * frameHeight)
                    {
                        npc.frame.Y = 5;
                    }
                }
            }
            else
            {
                if (++npc.frameCounter > 4)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += frameHeight;
                    if (npc.frame.Y >= 4 * frameHeight)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }*/

            /*npc.frame.Width = 28;
            //npc.frame.X = frameNum * frameHeight;
            npc.frame.X = ((int)actualFrame % 4) * npc.frame.Height;
            npc.frame.Y = (((int)actualFrame - ((int)actualFrame % 4)) / 3) * npc.frame.Height;*/

            npc.frameCounter += 0.15f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int Frame = (int)npc.frameCounter;
            npc.frame.Y = Frame * frameHeight;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CosmicApparitionHead"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CosmicApparitionBody"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CosmicApparitionArm"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CosmicApparitionArm"), 1f);
            }
            else
            {

                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.PurpleTorch, hitDirection, -2f, 0, default(Color), 1f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Shadowflame, hitDirection, -1f, 0, default(Color), 1f);
                }
            }

            // for (int k = 0; k < damage / npc.lifeMax * 50; k++)
            //     Dust.NewDust(npc.position, npc.width, npc.height, DustID.Shadowflame, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "The " + name;
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override void NPCLoot()
        {
            Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                int damage = expert ? 15 : 19;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 14f, direction.Y * 14f, ProjectileID.DD2DrakinShot, npc.damage, 1, Main.myPlayer, 0, 0);
            }

            if (!EternalWorld.downedCosmicApparition)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/CosmicEmperorLaugh"), (int)npc.position.X, (int)npc.position.Y);
                Main.NewText("Someone gazes upon your devotion to victory...", 220, 20, 220);
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
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Vexation>());
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ApparitionalRendingStaff>());
                }

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ApparitionalMatter>(), Main.rand.Next(15, 45));

            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (phase >= 1)
            {
                //tf does this supposed to mean
                int num159 = 1;
                float num160 = 0f;
                int num161 = num159;
                SpriteEffects spriteEffects = SpriteEffects.None;
                Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
                Texture2D texture2D4 = mod.GetTexture("NPCs/Boss/CosmicApparition/CosmicApparition2");
                int num1561 = texture2D4.Height / Main.npcFrameCount[npc.type];
                int y31 = num1561 * (int)npc.frameCounter;
                Microsoft.Xna.Framework.Rectangle rectangle2 = new Microsoft.Xna.Framework.Rectangle(0, y31, texture2D4.Width, num1561);
                Vector2 origin3 = rectangle2.Size() / 2f;
                SpriteEffects effects = spriteEffects;
                if (npc.spriteDirection > 0)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                float num165 = npc.rotation;
                Microsoft.Xna.Framework.Color color29 = npc.GetAlpha(color25);
                Main.spriteBatch.Draw(texture2D4, npc.position + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle2), color29, num165 + npc.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin3, npc.scale, effects, 0f);
                return false;
            }

            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                Texture2D shadowTexture = mod.GetTexture("NPCs/Boss/CosmicApparition/CosmicApparition_Shadow");
                spriteBatch.Draw(shadowTexture, drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
