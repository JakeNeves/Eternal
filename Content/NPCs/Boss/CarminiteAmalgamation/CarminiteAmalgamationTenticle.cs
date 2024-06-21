using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace Eternal.Content.NPCs.Boss.CarminiteAmalgamation
{
    public class CarminiteAmalgamationTenticle : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Carminite Tenticle");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        int moveTimer;
        int attackTimer = 0;

        const float acceleration = 0.2f;
        const float speed = 8f;

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 24;

            NPC.lifeMax = 150;
            NPC.damage = 15;
            NPC.defense = 16;
            NPC.knockBackResist = -1f;

            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.HitSound = SoundID.NPCHit13;
            NPC.DeathSound = SoundID.NPCDeath12;
        }

        

        public override void AI()
        {
            var entitySource = NPC.GetSource_FromAI();

            Player player = Main.player[NPC.target];

            attackTimer++;

            if (!NPC.AnyNPCs(ModContent.NPCType<CarminiteAmalgamation>()))
            {
                NPC.active = false;
            }
            NPC parent = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<CarminiteAmalgamation>())
                {
                    parent = Main.npc[i];
                    break;
                }
            }
            if (Main.netMode != 1)
            {
                NPC.localAI[0] -= 1f;
                if (NPC.localAI[0] <= 0f)
                {
                    NPC.localAI[0] = (float)Main.rand.Next(120, 480);
                    NPC.ai[0] = (float)Main.rand.Next(-100, 101);
                    NPC.ai[1] = (float)Main.rand.Next(-100, 101);
                    NPC.netUpdate = true;
                }
            }
            NPC.TargetClosest(true);
            float speed = 0.3f;
            float num780 = 300f;
            if ((double)parent.life < (double)parent.lifeMax * 0.25)
            {
                num780 += 100f;
            }
            if ((double)parent.life < (double)parent.lifeMax * 0.1)
            {
                num780 += 100f;
            }
            if (Main.expertMode)
            {
                float num781 = 1f - (float)NPC.life / (float)NPC.lifeMax;
                num780 += num781 * 400f;
                speed += 0.3f;
            }
            if (DifficultySystem.hellMode)
            {
                float num781 = 1f - (float)NPC.life / (float)NPC.lifeMax;
                num780 += num781 * 500f;
                speed += 0.4f;
            }
            if (!parent.active || ModContent.NPCType<CarminiteAmalgamation>() < 0)
            {
                NPC.active = false;
                return;
            }
            float targetX = parent.position.X + (float)(parent.width / 2);
            float targetY = parent.position.Y + (float)(parent.height / 2);
            Vector2 vector97 = new Vector2(targetX, targetY);
            float num784 = targetX + NPC.ai[0];
            float num785 = targetY + NPC.ai[1];
            float num786 = num784 - vector97.X;
            float num787 = num785 - vector97.Y;
            float num788 = (float)Math.Sqrt((double)(num786 * num786 + num787 * num787));
            num788 = num780 / num788;
            num786 *= num788;
            num787 *= num788;
            if (NPC.position.X < targetX + num786)
            {
                NPC.velocity.X = NPC.velocity.X + speed;
                if (NPC.velocity.X < 0f && num786 > 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                }
            }
            else if (NPC.position.X > targetX + num786)
            {
                NPC.velocity.X = NPC.velocity.X - speed;
                if (NPC.velocity.X > 0f && num786 < 0f)
                {
                    NPC.velocity.X = NPC.velocity.X * 0.9f;
                }
            }
            if (NPC.position.Y < targetY + num787)
            {
                NPC.velocity.Y = NPC.velocity.Y + speed;
                if (NPC.velocity.Y < 0f && num787 > 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
            }
            else if (NPC.position.Y > targetY + num787)
            {
                NPC.velocity.Y = NPC.velocity.Y - speed;
                if (NPC.velocity.Y > 0f && num787 < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.9f;
                }
            }
            float maxSpeed = 12f;
            if (NPC.velocity.X > maxSpeed)
            {
                NPC.velocity.X = maxSpeed;
            }
            if (NPC.velocity.X < -maxSpeed)
            {
                NPC.velocity.X = -maxSpeed;
            }
            if (NPC.velocity.Y > maxSpeed)
            {
                NPC.velocity.Y = maxSpeed;
            }
            if (NPC.velocity.Y < -maxSpeed)
            {
                NPC.velocity.Y = -maxSpeed;
            }
            if (num786 > 0f)
            {
                NPC.spriteDirection = 1;
                NPC.rotation = (float)Math.Atan2((double)num787, (double)num786);
            }
            if (num786 < 0f)
            {
                NPC.spriteDirection = -1;
                NPC.rotation = (float)Math.Atan2((double)num787, (double)num786) + 3.14f;
                return;
            }

            if (attackTimer == 100 || attackTimer == 110 || attackTimer == 120 || attackTimer == 130 || attackTimer == 140)
            {
                Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
                direction.Normalize();
                direction.X *= 8.5f;
                direction.Y *= 8.5f;
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ModContent.ProjectileType<CarminiteSludge>(), NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            if (attackTimer >= 200)
            {
                attackTimer = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/CarminiteAmalgamation/CarminiteAmalgamation_Chain").Value;
            NPC parentNPC = Main.npc[0];
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].type == ModContent.NPCType<CarminiteAmalgamation>())
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

            return true;
        }
    }
}
