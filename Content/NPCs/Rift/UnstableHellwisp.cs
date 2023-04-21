using Eternal.Common.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Rift
{
    public class UnstableHellwisp : ModNPC
    {
        int attackTimer = 0;

        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "Eternal/Assets/Textures/Bestiary/UnstableHellwisp_Preview",
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 0f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 66000;
            NPC.damage = 60;
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
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.UnderworldRift>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

                new FlavorTextBestiaryInfoElement("Upon opening the rift, some of the Hellwisps absorbed the unstability of the underworld's rift potential.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!RiftSystem.isRiftOpen)
                return SpawnCondition.Underworld.Chance * 0f;
            else
                return SpawnCondition.Underworld.Chance * 0.75f;
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
                Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100);
                if (Main.rand.NextBool(3))
                {
                    dust.noGravity = true;
                    dust.scale *= 3f;
                    dust.velocity.X *= 2f;
                    dust.velocity.Y *= 2f;
                }

                dust.scale *= 1.5f;
                dust.velocity *= 1.2f;
                dust.scale *= dustScale;
            }
            NPC.ai[0] += 1f;

            int maxdusts = 6;
            for (int i = 0; i < maxdusts; i++)
            {
                float dustDistance = 50;
                float dustSpeed = 8;
                Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                Dust vortex = Dust.NewDustPerfect(new Vector2(NPC.Center.X, NPC.Center.Y) + offset, DustID.PinkTorch, velocity, 0, default(Color), 1.5f);
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

            if (attackTimer == 200 || attackTimer == 215 || attackTimer == 230)
            {
                SoundEngine.PlaySound(SoundID.DD2_FlameburstTowerShot, NPC.position);
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.EyeLaser, NPC.damage, 1, Main.myPlayer, 0, 0);
            }

            if (attackTimer > 240)
            {
                attackTimer = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PinkTorch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                SoundEngine.PlaySound(SoundID.NPCDeath14, NPC.position);
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.PinkTorch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
