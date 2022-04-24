using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Eternal.Projectiles.Boss;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.BionicBosses.Omnicron
{
    //[AutoloadBossHead]
    public class OmnicronNeox : ModNPC
    {
        private Player player;

        float vectX = 0f;
        float vectY = 0f;

        int Phase;
        int attackTimer;
        int frameNum;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXM-2324 Omicron-N30X");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1240000;
            npc.damage = 60;
            npc.defense = 30;
            npc.knockBackResist = -1f;
            npc.width = 202;
            npc.height = 186;
            npc.value = Item.buyPrice(platinum: 3, gold: 20);
            npc.lavaImmune = true;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            music = MusicID.LunarBoss;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
            //bossBag = ItemType<CarmaniteScouterBag>();
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2530000;
            npc.damage = 120;
            npc.defense = 85;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 6060000;
                npc.damage = 240;
                npc.defense = 90;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                //Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasHead"), 1f);
                //Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasJaw"), 1f);
                //Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/AtlasBody"), 1f);
                CombatText.NewText(npc.Hitbox, Color.Orange, "SYSTEM FAILIURES DETECTED, CONTACTING MACHINES EXE-3068, EXE-3076 AND EXE-3090...", dramatic: true);
            }
            else
            {

                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Electric, hitDirection, -2f, 0, default(Color), 1f);
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.Fire, hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            Lighting.AddLight(npc.Center, 1.90f, 0.22f, 0.22f);

            player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    return true;
                }
            }

            return true;
        }

        public override void AI()
        {
            npc.rotation = npc.velocity.X * 0.06f;

            player = Main.player[npc.target];
            npc.TargetClosest(true);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int num728 = 6000;
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num728)
                {
                    npc.active = false;
                    npc.life = 0;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            if (npc.localAI[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.localAI[0] = 1f;
                int omniHook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OmnicronNeoxHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                omniHook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OmnicronNeoxHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                omniHook = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<OmnicronNeoxHook>(), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
            }

            if (npc.life < npc.lifeMax / 2)
            {
                Phase = 1;
                frameNum = 1;
                Lighting.AddLight(npc.Center, 1.17f, 0.10f, 0.32f);
            }
            else
            {
                frameNum = 0;
                Lighting.AddLight(npc.Center, 0f, 0f, 1.90f);
            }

            #region Plantera-Like Movement
            int[] array2 = new int[3];
            float num730 = 0f;
            float num731 = 0f;
            int num732 = 0;
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<OmnicronNeoxHook>())
                {
                    num730 += Main.npc[i].Center.X;
                    num731 += Main.npc[i].Center.Y;
                    array2[num732] = i;
                    num732++;
                    if (num732 > 2)
                    {
                        break;
                    }
                }
            }
            num730 /= (float)num732;
            num731 /= (float)num732;
            float num734 = 2.5f;
            float speed = 0.15f;
            float speedMultiplier = 2f;
            if (npc.life < npc.lifeMax / 2)
            {
                num734 = 5f;
                speed = 1.05f;
            }
            if (Main.expertMode)
            {
                num734 += 1f;
                num734 *= 1.1f;
            }
            if (EternalWorld.hellMode)
            {
                num734 += 1.1f;
                num734 *= 1.2f;
            }
            Vector2 vector91 = new Vector2(num730, num731);
            float targetX = Main.player[npc.target].Center.X - vector91.X;
            float targetY = Main.player[npc.target].Center.Y - vector91.Y;
            if (!player.active)
            {
                targetY *= -1f;
                targetX *= -1f;
                num734 += 8f;
            }
            float num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            int num739 = 500;
            if (Main.expertMode)
            {
                num739 += 100;
            }
            if (EternalWorld.hellMode)
            {
                num739 += 150;
            }
            if (num738 >= (float)num739)
            {
                num738 = (float)num739 / num738;
                targetX *= num738;
                targetY *= num738;
            }
            num730 += targetX;
            num731 += targetY;
            vector91 = new Vector2(npc.Center.X, npc.Center.Y);
            targetX = num730 - vector91.X;
            targetY = num731 - vector91.Y;
            num738 = (float)Math.Sqrt((double)(targetX * targetX + targetY * targetY));
            if (num738 < num734)
            {
                targetX = npc.velocity.X;
                targetY = npc.velocity.Y;
            }
            else
            {
                num738 = num734 / num738;
                targetX *= num738 * speedMultiplier;
                targetY *= num738 * speedMultiplier;
            }
            if (npc.velocity.X < targetX)
            {
                npc.velocity.X = npc.velocity.X + speed;
                if (npc.velocity.X < 0f && targetX > 0f)
                {
                    npc.velocity.X = npc.velocity.X + speed * 2f;
                }
            }
            else if (npc.velocity.X > targetX)
            {
                npc.velocity.X = npc.velocity.X - speed;
                if (npc.velocity.X > 0f && targetX < 0f)
                {
                    npc.velocity.X = npc.velocity.X - speed * 2f;
                }
            }
            if (npc.velocity.Y < targetY)
            {
                npc.velocity.Y = npc.velocity.Y + speed;
                if (npc.velocity.Y < 0f && targetY > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + speed * 2f;
                }
            }
            else if (npc.velocity.Y > targetY)
            {
                npc.velocity.Y = npc.velocity.Y - speed;
                if (npc.velocity.Y > 0f && targetY < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y - speed * 2f;
                }
            }
            Vector2 vector92 = new Vector2(npc.Center.X, npc.Center.Y);
            float num740 = Main.player[npc.target].Center.X - vector92.X;
            float num741 = Main.player[npc.target].Center.Y - vector92.Y;
            npc.rotation = (float)Math.Atan2((double)num741, (double)num740) + 1.57f;
            #endregion

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

            attackTimer++;

            if (Phase == 1)
            {
                if (attackTimer == 100 || attackTimer == 102 || attackTimer == 104 || attackTimer == 106 || attackTimer == 108 || attackTimer == 110 || attackTimer == 112 || attackTimer == 114 || attackTimer == 116 || attackTimer == 118 || attackTimer == 120 || attackTimer == 122)
                {
                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        int damage = Main.expertMode ? 15 : 17;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<OmicronFire>(), npc.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 130 || attackTimer == 135)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 0, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 0, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 8, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -8, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -4, 4, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -4, -4, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 4, -4, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 4, 4, ProjectileID.CultistBossFireBall, npc.damage, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 300)
                {
                    attackTimer = 0;
                }
            }
            else
            {
                if (attackTimer == 100 || attackTimer == 105 || attackTimer == 110 || attackTimer == 115)
                {

                    for (int i = 0; i < amountOfProjectiles; ++i)
                    {
                        float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                        float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                        int damage = Main.expertMode ? 15 : 17;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.CultistBossFireBall, npc.damage, 1, Main.myPlayer, 0, 0);
                    }
                }
                if (attackTimer == 200 || attackTimer == 250)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -8, 0, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 8, 0, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, 8, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -8, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -4, 4, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -4, -4, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 4, -4, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 4, 4, ModContent.ProjectileType<OmicronFire>(), npc.damage, 0, Main.myPlayer, 0f, 0f);
                }
                if (attackTimer == 300)
                {
                    attackTimer = 0;
                }
            }
        }

        public override void NPCLoot()
        {
            player = Main.player[npc.target];
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<OrionNeox>());

            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NeoxCore>(), Main.rand.Next(6, 16));
            }
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frame.Y = frameNum * frameHeight;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
