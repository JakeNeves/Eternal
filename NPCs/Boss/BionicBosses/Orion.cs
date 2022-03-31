using Eternal.Items.Materials;
using Eternal.Items.Potions;
using Eternal.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.BionicBosses
{
    [AutoloadBossHead]
    public class Orion : ModNPC
    {

        #region Fundementals
        const float Speed = 20.05f;
        const float Acceleration = 0.4f;
        int Timer;

        int AttackTimer = 0;

        bool dontAttack = false;
        bool justSummonedProbe = false;
        #endregion

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("XR-2008 Orion-X5");
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

        public override void SetDefaults()
        {
            npc.width = 198;
            npc.height = 198;
            npc.lifeMax = 620000;
            npc.defense = 30;
            npc.damage = 60;
            npc.aiStyle = -1;
            npc.knockBackResist = -1f;
            npc.value = Item.buyPrice(platinum: 3, gold: 20);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.boss = true;
            music = MusicID.Boss4;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.ShadowFlame] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.Frozen] = true;
            npc.buffImmune[BuffID.Chilled] = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                CombatText.NewText(npc.Hitbox, Color.Red, "SYSTEM FAILIURES DETECTED, SELF-DESTRUCT INITIATED...", dramatic: true);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/OrionRing1"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/OrionRing2"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/OrionRing3"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/OrionCore1"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/OrionCore2"), 1f);
                Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/OrionCore3"), 1f);
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 1240000;
            npc.damage = 120;
            npc.defense = 60;

            if (EternalWorld.hellMode)
            {
                npc.lifeMax = 2530000;
                npc.damage = 240;
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<PristineHealingPotion>();
        }

        public override bool PreAI()
        {
            Lighting.AddLight(npc.Center, 1.90f, 0.22f, 0.22f);

            npc.TargetClosest(true);
            npc.spriteDirection = npc.direction;
            npc.rotation += npc.velocity.X * 0.1f;
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

            return true;
        }

        public override void AI()
        {
            AttackTimer++;
            if (npc.life < npc.lifeMax / 2)
            {
                DoAttacksPhase2();
            }
            else
            {
                DoAttacksPhase1();
            }
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
                amountOfProjectiles = 2;
            }
            else if (EternalWorld.hellMode)
            {
                amountOfProjectiles = Main.rand.Next(4, 8);
            }
            else
            {
                amountOfProjectiles = 1;
            }

            if(AttackTimer == 100 || AttackTimer == 105 || AttackTimer == 110 || AttackTimer == 115 || AttackTimer == 120 || AttackTimer == 125 || AttackTimer == 130)
            {
                for (int i = 0; i < amountOfProjectiles; ++i)
                {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    int damage = Main.expertMode ? 15 : 17;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ProjectileID.DeathLaser, npc.damage, 1, Main.myPlayer, 0, 0);
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
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<OrionSaw>(), npc.damage, 1, Main.myPlayer, 0, 0);
                }
            }
            if (AttackTimer == 231)
            {
                AttackTimer = 0;
            }
        }

        private void DoAttacksPhase2()
        {
            if (!justSummonedProbe)
            {
                CombatText.NewText(npc.Hitbox, Color.Red, "PROTOCOL 30 INITIATED, DEPLOYING SUPPORT PROBE...", dramatic: true);
                NPC.NewNPC((int)npc.position.X - 64, (int)npc.position.Y, ModContent.NPCType<OrionProbe>());
                justSummonedProbe = true;
            }

            if (NPC.AnyNPCs(ModContent.NPCType<OrionProbe>()))
            {
                npc.dontTakeDamage = true;
                dontAttack = true;
            }
            if (!NPC.AnyNPCs(ModContent.NPCType<OrionProbe>()))
            {
                npc.dontTakeDamage = false;
                dontAttack = false;
            }

            if (!dontAttack)
            {

            }
        }

        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<PiCore>(), Main.rand.Next(20, 40));

            if (Main.rand.Next(6) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WeatheredPlating>(), Main.rand.Next(3, 9));
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

    }
}
