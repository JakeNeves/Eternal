using Eternal.Content.Projectiles.Boss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Boss.CosmicEmperor
{
    public class GalaxiaStarwisp : ModNPC
    {
        int attackTimer = 0;

        Vector2 CircleDirc = new Vector2(0.0f, 16f);

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Galaxia Starwisp");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 4400;
            NPC.damage = 120;
            NPC.defense = 45;
            NPC.knockBackResist = -1f;
            NPC.width = 6;
            NPC.height = 6;
            NPC.alpha = 255;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.value = Item.sellPrice(gold: 30);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);

            attackTimer++;
            Attack();

            float dustScale = 1f;
            if (NPC.ai[0] == 0f)
                dustScale = 0.25f;
            else if (NPC.ai[0] == 1f)
                dustScale = 0.5f;
            else if (NPC.ai[0] == 2f)
                dustScale = 0.75f;

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.DemonTorch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100);
                if (Main.rand.NextBool(3))
                {
                    dust.noGravity = true;
                    dust.scale *= 3f;
                    dust.velocity.X *= 2f;
                    dust.velocity.Y *= 2f;
                }

                dust.scale *= 1.25f;
                dust.velocity *= 1.2f;
                dust.scale *= dustScale;
            }
            NPC.ai[0] += 1f;

            int maxdusts = 6;
            for (int i = 0; i < maxdusts; i++)
            {
                float dustDistance = 100;
                float dustSpeed = 4;
                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y) + offset, DustID.DemonTorch, velocity, 0, default(Color), 1.5f);
                vortex.noGravity = true;
            }
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (attackTimer > 200 && attackTimer < 230)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);

            }
            if (attackTimer > 300 && attackTimer < 430)
            {
                CircleDirc = Utils.RotatedBy(CircleDirc, 0.10000000149011612, new Vector2());
                int index5 = Projectile.NewProjectile(entitySource, NPC.Center, CircleDirc, ProjectileID.EyeLaser, NPC.damage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                Main.projectile[index5].tileCollide = false;
                Main.projectile[index5].timeLeft = 300;
            }

            if (attackTimer > 430)
            {
                attackTimer = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.DemonTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.DemonTorch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
