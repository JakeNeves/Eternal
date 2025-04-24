using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Eternal.Content.Projectiles.Enemy;

namespace Eternal.Content.NPCs.Boss.TheGlare
{
    public class GlareHeart : ModNPC
    {
        ref float AttackTimer => ref NPC.ai[1];

        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 100000;
            NPC.damage = 30;
            NPC.defense = 50;
            NPC.knockBackResist = 0f;
            NPC.width = 84;
            NPC.height = 72;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.npcSlots = 6;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (Main.netMode == NetmodeID.Server)
                return;

            if (NPC.life <= 0)
            {
                if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                {
                    int gore1 = Mod.Find<ModGore>("GlareHeartGehenna1").Type;
                    int gore2 = Mod.Find<ModGore>("GlareHeartGehenna2").Type;
                    int gore3 = Mod.Find<ModGore>("GlareHeartGehenna3").Type;

                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore1);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore2);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore3);
                }
                else
                {
                    int gore1 = Mod.Find<ModGore>("GlareHeart1").Type;
                    int gore2 = Mod.Find<ModGore>("GlareHeart2").Type;
                    int gore3 = Mod.Find<ModGore>("GlareHeart3").Type;

                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore1);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore2);
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(-4, 4)), gore3);
                }
            }

            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Torch, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
                }
            }
            else
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.PurpleTorch, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
                }
            } 
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * balance * bossAdjustment);
            NPC.damage = (int)(NPC.damage * balance * bossAdjustment);
        }

        public override bool PreAI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }
            float speed;

            if (DifficultySystem.hellMode)
            {
                speed = 10f;
            }
            else if (Main.expertMode)
            {
                speed = 8f;
            }
            else
            {
                speed = 6.5f;
            }

            float acceleration = 0.10f;
            Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
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
            if (NPC.velocity.X < xDir)
            {
                NPC.velocity.X = NPC.velocity.X + acceleration;
                if (NPC.velocity.X < 0 && xDir > 0)
                    NPC.velocity.X = NPC.velocity.X + acceleration;
            }
            else if (NPC.velocity.X > xDir)
            {
                NPC.velocity.X = NPC.velocity.X - acceleration;
                if (NPC.velocity.X > 0 && xDir < 0)
                    NPC.velocity.X = NPC.velocity.X - acceleration;
            }
            if (NPC.velocity.Y < yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y + acceleration;
                if (NPC.velocity.Y < 0 && yDir > 0)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
            }
            else if (NPC.velocity.Y > yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y - acceleration;
                if (NPC.velocity.Y > 0 && yDir < 0)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
            }

            return true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.None;
        }

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            NPC parentNPC = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<TheGlare>())
                {
                    parentNPC = Main.npc[i];
                    break;
                }
            }

            if (!parentNPC.active)
                NPC.active = false;

            #region attacks
            AttackTimer++;

            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
            {
                if (AttackTimer == 200)
                {
                    if (!Main.dedServ)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot);

                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.Fireball, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.Fireball, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.Fireball, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.Fireball, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -6f, ProjectileID.Fireball, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 6f, -12f, ProjectileID.Fireball, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 6f, ProjectileID.Fireball, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 6f, 12f, ProjectileID.Fireball, NPC.damage, 0);
                    }
                }

                if (AttackTimer >= 300 && AttackTimer < 350)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                }

                if (AttackTimer == 300)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                    }
                }

                if (AttackTimer == 325)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                    }
                }

                if (AttackTimer == 350)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, -12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -12f, ModContent.ProjectileType<Sanguinebeam>(), NPC.damage, 0);
                    }
                }
            }
            else
            {
                if (AttackTimer == 200)
                {
                    if (!Main.dedServ)
                    {
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot);

                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -6f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 6f, -12f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 6f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 6f, 12f, ProjectileID.ShadowFlame, NPC.damage / 2, 0);
                    }
                }

                if (AttackTimer >= 300 && AttackTimer < 350)
                {
                    NPC.velocity.X = 0;
                    NPC.velocity.Y = 0;
                }

                if (AttackTimer == 300)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 12f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 12f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                    }
                }

                if (AttackTimer == 325)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                    }
                }

                if (AttackTimer == 350)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, -12f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, -12f, ProjectileID.ShadowBeamHostile, NPC.damage, 0);
                    }
                }
            }

            if (AttackTimer >= 400)
                AttackTimer = 0;
            #endregion
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Texture2D texture;

            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/TheGlare/TheGlareGehenna_Chain").Value;
            else
                texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/TheGlare/TheGlare_Chain").Value;

            NPC parentNPC = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<TheGlare>())
                {
                    parentNPC = Main.npc[i];
                    break;
                }
            }
            Vector2 position = NPC.Center;
            Vector2 mountedCenter = parentNPC.Center;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = NPC.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            if (ModContent.GetInstance<ZoneSystem>().zoneGehenna)
            {
                //tf does this supposed to mean
                int num159 = 1;
                float num160 = 0f;
                int num161 = num159;
                SpriteEffects spriteEffects = SpriteEffects.None;
                Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)NPC.position.X + (double)NPC.width * 0.5) / 16, (int)(((double)NPC.position.Y + (double)NPC.height * 0.5) / 16.0));
                Texture2D texture2D4 = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/TheGlare/GlareHeartGehenna").Value;
                int num1561 = texture2D4.Height / Main.npcFrameCount[NPC.type];
                int y31 = num1561 * (int)NPC.frameCounter;
                Microsoft.Xna.Framework.Rectangle rectangle2 = new Microsoft.Xna.Framework.Rectangle(0, y31, texture2D4.Width, num1561);
                Vector2 origin3 = rectangle2.Size() / 2f;
                SpriteEffects effects = spriteEffects;
                if (NPC.spriteDirection > 0)
                {
                    effects = SpriteEffects.FlipHorizontally;
                }
                float num165 = NPC.rotation;
                Microsoft.Xna.Framework.Color color29 = NPC.GetAlpha(color25);
                Main.spriteBatch.Draw(texture2D4, NPC.position + NPC.Size / 2f - Main.screenPosition + new Vector2(0f, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle2), color29, num165 + NPC.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin3, NPC.scale, effects, 0f);
                return false;
            }

            return true;
        }
    }
}

