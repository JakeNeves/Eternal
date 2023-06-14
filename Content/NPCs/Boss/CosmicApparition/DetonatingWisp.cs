using Eternal.Common.Systems;
using Eternal.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.RGB;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CosmicApparition
{
    public class DetonatingWisp : ModNPC
    {

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void SetDefaults()
        {
            NPC.width = 48;
            NPC.height = 48;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath3;
            NPC.damage = 90;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
            NPC.alpha = 255;
            NPCID.Sets.ProjectileNPC[NPC.type] = true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
            if (NPC.life < 0)
            {
                _ = NPC.Center;
                for (int num120 = 0; num120 < 60; num120++)
                {
                    int num121 = 25;
                    _ = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * Main.rand.Next(24, 41) / 8f;
                    int num122 = Dust.NewDust(NPC.Center - Vector2.One * num121, num121 * 2, num121 * 2, DustID.DemonTorch);
                    Dust dust154 = Main.dust[num122];
                    Vector2 vector7 = Vector2.Normalize(dust154.position - NPC.Center);
                    dust154.position = NPC.Center + vector7 * 25f * NPC.scale;
                    if (num120 < 30)
                    {
                        dust154.velocity = vector7 * dust154.velocity.Length();
                    }
                    else
                    {
                        dust154.velocity = vector7 * Main.rand.Next(45, 91) / 10f;
                    }
                    dust154.noGravity = true;
                    dust154.scale = 0.7f;
                }

                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Shadowflame, NPC.oldVelocity.X * 0.25f, NPC.oldVelocity.Y * 0.25f);
                }
            }
        }

        public override void AI()
        {
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            NPC.alpha -= 10;

            CheckActive();
            Move(new Vector2(0, 0f));
            Target();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
            target.ApplyDamageToNPC(NPC, 1, 0, 0, false);
            target.AddBuff(ModContent.BuffType<ApparitionalWither>(), 1 * 60 * 60, false);
        }

        private void Move(Vector2 offset)
        {
            float speed;

            if (Main.expertMode)
            {
                speed = 12f;
            }
            else if (DifficultySystem.hellMode)
            {
                speed = 16f;
            }
            else
            {
                speed = 8f;
            }

            Player player = Main.player[NPC.target];
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - NPC.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f;
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            NPC.velocity = move;
        }

        /* public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            var wispShader = GameShaders.Misc["Eternal:ApparitionalParticle"];
            wispShader.UseOpacity(1f - (NPC.ai[0] - 30f) / 150f);

            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        } */

        public override bool CheckActive()
        {
            Player player = Main.player[NPC.target];
            return !player.active || player.dead;
        }

        private void Target()
        {
            Player player = Main.player[NPC.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }
    }
}
