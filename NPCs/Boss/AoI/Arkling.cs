using System;
using Eternal.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Boss.AoI
{
    public class Arkling : ModNPC
    {
        
        public override void SetDefaults()
        {
            npc.lifeMax = 4000;
            npc.width = 38;
            npc.height = 72;
            npc.damage = 10;
            npc.defense = 30;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.DD2_DarkMageHealImpact;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.lavaImmune = true;
            npc.noTileCollide = true;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = npc.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                    Dust dust = Dust.NewDustPerfect(npc.position, ModContent.DustType<ArkEnergy>());
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - npc.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }
            }
            else
            {
                for (int k = 0; k < damage / npc.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(npc.Center, npc.width, npc.height, DustID.GreenTorch, hitDirection, 0f, 0, default(Color), 0.7f);
                }
            }
        }

        public override void AI()
        {
            npc.rotation = npc.velocity.X * 0.08f;
            float speed;
            if (EternalWorld.hellMode)
            {
                speed = 18f;
            }
            else
            {
                speed = 14f;
            }
            float acceleration = 0.10f;
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
        }

    }
}
