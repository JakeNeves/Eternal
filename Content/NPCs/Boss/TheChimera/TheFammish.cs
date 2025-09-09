using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.TheChimera
{
    public class TheFammish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 3;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 42;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.damage = 20;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 14;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life < 0)
            {
                _ = NPC.Center;
                for (int num120 = 0; num120 < 60; num120++)
                {
                    int num121 = 25;
                    _ = ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * Main.rand.Next(24, 41) / 8f;
                    int num122 = Dust.NewDust(NPC.Center - Vector2.One * num121, num121 * 2, num121 * 2, DustID.GreenBlood);
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
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.GreenBlood, NPC.oldVelocity.X * 0.25f, NPC.oldVelocity.Y * 0.25f);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frameCounter %= Main.npcFrameCount[NPC.type];
            int Frame = (int)NPC.frameCounter;
            NPC.frame.Y = Frame * frameHeight;
        }

        public override void AI()
        {
            NPC.rotation = NPC.velocity.ToRotation() + MathHelper.ToRadians(90f);

            Player target = Main.player[NPC.target];

            if (NPC.frame.Y == 1)
            {
                if (Main.rand.NextBool(2))
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.NPCDeath22, NPC.position);

                    Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                    direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

                    var entitySource = NPC.GetSource_FromAI();
                    var projectile = Projectile.NewProjectileDirect(entitySource, NPC.Center, direction * 1, ModContent.ProjectileType<BloodClot>(), NPC.damage, 0, Main.myPlayer);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (Main.rand.NextBool(2))
                target.AddBuff(BuffID.Bleeding, 1 * 60 * 30);

            SoundEngine.PlaySound(SoundID.NPCDeath1, NPC.position);
            target.ApplyDamageToNPC(NPC, 1, 0, 0, false);
        }
    }
}
