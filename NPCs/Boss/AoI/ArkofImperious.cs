using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Eternal.Items.Weapons.Ranged;
using Eternal.Items.Ammo;
using Eternal.Items.BossBags;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Materials;
using Eternal.Projectiles.Boss;
using Eternal.Items.Potions;
using Microsoft.Xna.Framework.Graphics;
using Eternal.Dusts;

namespace Eternal.NPCs.Boss.AoI
{
    [AutoloadBossHead]
    public class ArkofImperious : ModNPC
    {
        private Player player;

        bool isDashing = false;

        bool justSpawnedCircle = false;

        #region Fundimentals
        float speed = 16;
        int Phase = 0;
        int AttackTimer = 0;
        int moveTimer;
        float acceleration = 0.2f;
        #endregion

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ark of Imperious");
            NPCID.Sets.TrailCacheLength[npc.type] = 12;
            NPCID.Sets.TrailingMode[npc.type] = 0;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.width = 68;
            npc.height = 230;
            npc.lifeMax = 1200000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.boss = true;
            if (!ModContent.GetInstance<EternalConfig>().originalMusic)
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/New/ImperiousStrike");
            }
            else
            {
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/BladeofBrutality");
            }
            npc.defense = 70;
            npc.damage = 62;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.knockBackResist = 0f;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            bossBag = ModContent.ItemType<AoIBag>();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width * 0.5f, npc.height * 0.5f);
            for (int k = 0; k < npc.oldPos.Length; k++)
            {
                Vector2 drawPos = npc.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, npc.gfxOffY);
                Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, null, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            }
            return true;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Bleeding, 180, false);
            player.AddBuff(BuffID.BrokenArmor, 180, false);
            if (EternalWorld.hellMode)
            {
                player.AddBuff(BuffID.Cursed, 180, false);
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2400000;
            npc.defense = 72;
            if(EternalWorld.hellMode)
            {
                npc.lifeMax = 3600000;
                npc.defense = 74;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            if (Phase == 0)
            {
                DoAttacksPhase1();
            }
            if (Phase == 1)
            {
                DoAttacksPhase2();
            }
            if (Phase == 3)
            {
                DoAttacksPhase3();
            }

            Movement();

            return true;
        }

        private void Movement()
        {
            float speed = 48f;
            float acceleration = 0.20f;
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);

            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            Player player = Main.player[npc.target];
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                npc.active = false;
            }

            if (isDashing)
            {
                speed = 48f;
                acceleration = 0.20f;
                npc.rotation = npc.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            else
            {
                speed = 12f;
                acceleration = 0.10f;
                if (EternalWorld.hellMode)
                {
                    npc.rotation = npc.velocity.X * 0.06f;
                }
                else
                {
                    npc.rotation = npc.velocity.X * 0.03f;
                }

            }

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

        public override void AI()
        {
            Vector2 vector2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float xDir = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120) - vector2.Y;
            float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
            }
            if (npc.life < npc.lifeMax / 4)
            {
                Phase = 2;
            }

            AttackTimer++;

            if (Phase == 1)
            {
                if (!justSpawnedCircle)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 0, ModContent.ProjectileType<AoICircle>(), npc.damage, 0, 0, 0f, npc.whoAmI);
                    justSpawnedCircle = true;
                }
            }

            if (Phase == 2)
            {
                int maxDist;

                if(Main.expertMode)
                {
                    maxDist = 1000;
                }
                else if (EternalWorld.hellMode)
                {
                    maxDist = 900;
                }
                else
                {
                    maxDist = 2000;
                }

                // ripped from another mod, credit to the person who wrote this
                for (int i = 0; i < 120; i++)
                {
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                    Dust dust = Main.dust[Dust.NewDust(npc.Center + offset, 0, 0, ModContent.DustType<ArkEnergy>(), 0, 0, 100)];
                    dust.noGravity = true;
                }
                for (int i = 0; i < Main.player.Length; i++)
                {
                    Player player = Main.player[i];
                    if (player.active && !player.dead && Vector2.Distance(player.Center, npc.Center) > maxDist)
                    {
                        Vector2 toTarget = new Vector2(npc.Center.X - player.Center.X, npc.Center.Y - player.Center.Y);
                        toTarget.Normalize();
                        float speed = Vector2.Distance(player.Center, npc.Center) > maxDist + 500 ? 1f : 0.5f;
                        player.velocity += toTarget * 0.5f;

                        player.dashDelay = 2;
                        player.grappling[0] = -1;
                        player.grapCount = 0;
                        for (int p = 0; p < Main.projectile.Length; p++)
                        {
                            if (Main.projectile[p].active && Main.projectile[p].owner == player.whoAmI && Main.projectile[p].aiStyle == 7)
                            {
                                Main.projectile[p].Kill();
                            }
                        }
                    }
                }
                int maxdusts = 6;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 200;
                    float dustSpeed = 8;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(new Vector2(npc.Center.X, npc.Center.Y) + offset, ModContent.DustType<ArkEnergy>(), velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;
                }
            }

        }

        private void DoAttacksPhase1()
        {
            if ((AttackTimer == 100 || AttackTimer == 150 || AttackTimer == 175))
            {
                //NPC.NewNPC((int)npc.Center.X - 20, (int)npc.Center.Y, ModContent.NPCType<Arkling>());

                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, -12, 0, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 12, 0, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, 12, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 80, npc.position.Y + 80, 0, -12, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, -8, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, -8, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, -8, 8, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
                Projectile.NewProjectile(npc.position.X + 40, npc.position.Y + 40, 8, 8, ModContent.ProjectileType<ArkArrowHostile>(), 6, 0, Main.myPlayer, 0f, 0f);
            }
            if(AttackTimer == 300)
            {
                isDashing = true;
            }
            else if (AttackTimer == 475)
            {
                isDashing = false;
                AttackTimer = 0;
            }
        }

        private void DoAttacksPhase2()
        {
            float projRot = MathHelper.PiOver2;

            int shootTimer = 4;
            int rate = 60;

            if (AttackTimer >= 100 && AttackTimer < 180)
            { 
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    projRot += 0.01f;
                    if (--shootTimer <= 0)
                    {
                        shootTimer = rate;
                        var shootPos = npc.Center + new Vector2(0, 17);
                        var shootVel = new Vector2(0, 7).RotatedBy(MathHelper.PiOver2);
                        int[] i = {
                            Projectile.NewProjectile(shootPos, shootVel, ModContent.ProjectileType<ArkArrowHostile>(), 90, 1f),
                            Projectile.NewProjectile(shootPos, shootVel.RotatedBy(MathHelper.PiOver2), ModContent.ProjectileType<ArkArrowHostile>(), 90, 1f),
                            Projectile.NewProjectile(shootPos, shootVel.RotatedBy(MathHelper.Pi), ModContent.ProjectileType<ArkArrowHostile>(), 90, 1f),
                            Projectile.NewProjectile(shootPos, shootVel.RotatedBy(-MathHelper.PiOver2), ModContent.ProjectileType<ArkArrowHostile>(), 90, 1f)
                        };
                        for (int l = 0; l < i.Length; l++)
                        {
                            Main.projectile[i[l]].hostile = true;
                            Main.projectile[i[l]].tileCollide = false;
                        }
                    }
                }
            }
        }

        private void DoAttacksPhase3()
        {

        }

        public override void NPCLoot()
        {
            if(!EternalWorld.downedArkOfImperious)
            {
                Main.NewText("The stars are calling upon you...", 0, 90, 216);
                EternalWorld.downedArkOfImperious = true;
            }

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(1) == 0)
                {
                   Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Arkbow>());
                   Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ArkArrow>(), Main.rand.Next(30, 90));
                }
                if (Main.rand.Next(2) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DormantHeroSword>());
                }
                if (Main.rand.Next(3) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheImperiousCohort>());
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<TheEnigma>());
                }

                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ArkaniumCompound>(), Main.rand.Next(20, 40));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
