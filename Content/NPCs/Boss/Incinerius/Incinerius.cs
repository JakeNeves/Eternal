using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.Incinerius
{
    [AutoloadBossHead]
    public class Incinerius : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.TrailCacheLength[NPC.type] = 14;
            NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        #region Fundementals
        const int ShootDamage = 9;
        const float ShootKnockback = 0f;
        const int ShootDirection = 5;

        const float Speed = 6f;
        const float Acceleration = 2f;
        int Timer;
        #endregion

        public override void SetDefaults()
        {
            NPC.width = 99;
            NPC.height = 119;
            NPC.boss = true;
            Music = MusicID.Boss3;
            NPC.aiStyle = -1;
            NPC.damage = 12;
            NPC.defense = 20;
            NPC.lifeMax = 480000;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Electrified] = true;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = null;
            NPC.DeathSound = SoundID.NPCDeath42;
        }

        public Vector2 bossCenter
        {
            get { return NPC.Center; }
            set { NPC.position = value - new Vector2(NPC.width / 2, NPC.height / 2); }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!NPC.downedMoonlord)
            {
                potionType = ItemID.GreaterHealingPotion;
            }
            else
            {
                potionType = ItemID.None;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (Main.masterMode)
            {
                NPC.lifeMax = 108000;
                NPC.damage = 16;
                NPC.defense = 24;
            }
            else if (DifficultySystem.hellMode)
            {
                NPC.lifeMax = 120000;
                NPC.damage = 18;
                NPC.defense = 26;
            }
            else
            {
                NPC.lifeMax = 96000;
                NPC.damage = 14;
                NPC.defense = 22;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            SoundEngine.PlaySound(SoundID.Tink, NPC.position);
            if (NPC.life <= 0)
            {

            }
        }

        private Player player;

        public override void AI()
        {
            NPC.spriteDirection = NPC.direction;

            Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;
            Player player = Main.player[NPC.target];
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
                NPC.active = false;
            }
            Timer++;
            if (Timer >= 0)
            {
                Vector2 StartPosition = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float DirectionX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2 - StartPosition.X;
                float DirectionY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120 - StartPosition.Y;
                float Length = (float)Math.Sqrt(DirectionX * DirectionX + DirectionY * DirectionY);
                float Num = Speed / Length;
                DirectionX = DirectionX * Num;
                DirectionY = DirectionY * Num;
                if (NPC.velocity.X < DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X + Acceleration;
                    if (NPC.velocity.X < 0 && DirectionX > 0)
                        NPC.velocity.X = NPC.velocity.X + Acceleration;
                }
                else if (NPC.velocity.X > DirectionX)
                {
                    NPC.velocity.X = NPC.velocity.X - Acceleration;
                    if (NPC.velocity.X > 0 && DirectionX < 0)
                        NPC.velocity.X = NPC.velocity.X - Acceleration;
                }
                if (NPC.velocity.Y < DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                    if (NPC.velocity.Y < 0 && DirectionY > 0)
                        NPC.velocity.Y = NPC.velocity.Y + Acceleration;
                }
                else if (NPC.velocity.Y > DirectionY)
                {
                    NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                    if (NPC.velocity.Y > 0 && DirectionY < 0)
                        NPC.velocity.Y = NPC.velocity.Y - Acceleration;
                }
                if (Main.rand.NextBool(36))
                {
                    Vector2 StartPosition2 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height / 2));
                    float BossRotation = (float)Math.Atan2(StartPosition2.Y - (Main.player[NPC.target].position.Y + (Main.player[NPC.target].height * 0.5f)), StartPosition2.X - (Main.player[NPC.target].position.X + (Main.player[NPC.target].width * 0.5f)));
                    NPC.velocity.X = (float)(Math.Cos(BossRotation) * 9) * -1;
                    NPC.velocity.Y = (float)(Math.Sin(BossRotation) * 9) * -1;
                    NPC.netUpdate = true;
                }
            }

            #region Death Dialogue
            if (player.dead)
            {
                if (Main.rand.NextBool(1))
                {
                    Main.NewText("Surprise, you have fallen before me!", 215, 95, 0);
                }
                if (Main.rand.NextBool(2))
                {
                    Main.NewText("Did you like the improvements that I made?", 215, 95, 0);
                }
                if (Main.rand.NextBool(3))
                {
                    Main.NewText("I was the first construct of the underworld, but not the first flame.", 215, 95, 0);
                }
                if (Main.rand.NextBool(4))
                {
                    Main.NewText(player.name + ", your mistakes don't get you far... Remember that!", 215, 95, 0);
                }
                if (Main.rand.NextBool(5))
                {
                    Main.NewText("That was terrible...", 215, 95, 0);
                }
                if (Main.rand.NextBool(6))
                {
                    Main.NewText("Maybe if you were skilled enough, I wouldn't have called you out for skill issue right now...", 215, 95, 0);
                }
                NPC.active = false;
            }
            #endregion
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color lightColor)
        {
            Main.instance.LoadProjectile(NPC.type);
            Texture2D texture = ModContent.Request<Texture2D>("Eternal/Content/NPCs/Boss/Incinerius/Incinerius_Shadow").Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(lightColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
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
