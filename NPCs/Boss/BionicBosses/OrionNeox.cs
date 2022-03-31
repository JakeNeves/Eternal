using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Eternal.NPCs.Boss.BionicBosses.Omnicron;
using Eternal.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.BionicBosses
{
    //[AutoloadBossHead]
    public class OrionNeox : ModNPC
    {
        private Player player;

        int AttackTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("EXR-2308 Orion-N30X");
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

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.Purple, "SYSTEM FAILIURES DETECTED, CONTACTING MACHINES EXR-2324", dramatic: true);
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

        public override void SetDefaults()
        {
            npc.width = 222;
            npc.height = 222;
            npc.lifeMax = 1240000;
            npc.defense = 80;
            npc.damage = 120;
            npc.aiStyle = -1;
            npc.knockBackResist = -1f;
            npc.value = Item.buyPrice(platinum: 6, gold: 40);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            music = MusicID.LunarBoss;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2530000;
            npc.damage = 240;
            npc.defense = 85;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 6060000;
                npc.damage = 260;
                npc.defense = 90;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            Lighting.AddLight(npc.Center, 0.77f, 0.18f, 0.86f);

            npc.rotation += npc.velocity.X * 0.1f;

            npc.netUpdate = true;
            player = Main.player[npc.target];
            npc.dontTakeDamage = false;
            float speed = 36.25f;
            float acceleration = 0.4f;
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
            DespawnHandler();

            return true;
        }

        private void DespawnHandler()
        {
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
                    return;
                }
            }
        }

        public override void AI()
        {
            AttackTimer++;
            DoAttacksPhase1();
        }

        private void DoAttacksPhase1()
        {
            Vector2 direction = Main.player[npc.target].Center - npc.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;
            int amountOfProjectiles;

            if (Main.expertMode)
            {
                amountOfProjectiles = Main.rand.Next(4, 8);
            }
            else if (EternalWorld.hellMode)
            {
                amountOfProjectiles = Main.rand.Next(8, 12);
            }
            else
            {
                amountOfProjectiles = Main.rand.Next(2, 4);
            }

            if (AttackTimer == 100 || AttackTimer == 105 || AttackTimer == 110 || AttackTimer == 115 || AttackTimer == 120 || AttackTimer == 125 || AttackTimer == 130)
            {
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    int damage = Main.expertMode ? 15 : 17;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<OrionNeoxLaser>(), npc.damage / 2, 1, Main.myPlayer, 0, 0);
                }
            }
            if (AttackTimer == 200 || AttackTimer == 205 || AttackTimer == 210 || AttackTimer == 215 || AttackTimer == 220 || AttackTimer == 225 || AttackTimer == 230)
            {
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-100, 100) * 0.01f;
                    float B = (float)Main.rand.Next(-100, 100) * 0.01f;
                    int damage = Main.expertMode ? 15 : 17;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Main.PlaySound(SoundID.DD2_KoboldExplosion, npc.position);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<OrionNeoxBomb>(), npc.damage / 2, 1, Main.myPlayer, 0, 0);
                }
            }
            if (AttackTimer == 231)
            {
                AttackTimer = 0;
            }
        }

        public override void NPCLoot()
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<OmnicronNeox>());

            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NeoxCore>(), Main.rand.Next(6, 16));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
