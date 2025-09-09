using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CarminiteAmalgamation
{
    public class CarminiteToothBall : ModNPC
    {
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
            NPC.width = 50;
            NPC.height = 50;
            NPC.lifeMax = 1;
            NPC.HitSound = SoundID.NPCHit13;
            NPC.DeathSound = SoundID.NPCDeath12;
            NPC.damage = 10;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14;
            NPCID.Sets.ProjectileNPC[NPC.type] = true;
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
                    int num122 = Dust.NewDust(NPC.Center - Vector2.One * num121, num121 * 2, num121 * 2, DustID.Blood);
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
                    Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Blood, NPC.oldVelocity.X * 0.25f, NPC.oldVelocity.Y * 0.25f);
                }
            }
        }

        public override void AI()
        {
            NPC.rotation += 0.15f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1, NPC.position);
            target.ApplyDamageToNPC(NPC, 1, 0, 0, false);
        }
    }
}
