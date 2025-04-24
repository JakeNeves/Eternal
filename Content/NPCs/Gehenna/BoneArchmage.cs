using Eternal.Common.Systems;
using Eternal.Content.Biomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Gehenna
{
    public class BoneArchmage : ModNPC
    {
        int attackTimer = 0;
        int frameNum;

        static int attackDelayMax = 6;
        int attackDelay = attackDelayMax;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 5000;
            NPC.damage = 40;
            NPC.defense = 60;
            NPC.knockBackResist = 0f;
            NPC.width = 28;
            NPC.height = 46;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = Item.sellPrice(gold: 15);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.lavaImmune = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.Gehenna>().Type ];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                new FlavorTextBestiaryInfoElement("They can be very deadly with their 4-way attacks...")
            ]);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!DownedBossSystem.downedCosmicApparition && !ModContent.GetInstance<ZoneSystem>().zoneGehenna)
                return SpawnCondition.Underworld.Chance * 0f;
            else
                return SpawnCondition.Underworld.Chance * 0.25f;
        }

        public override void AI()
        {
            Player target = Main.player[NPC.target];
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;

            Lighting.AddLight(NPC.position, 2.15f, 0.95f, 0f);

            attackTimer++;
            Attack();
        }

        private void Attack()
        {
            var entitySource = NPC.GetSource_FromAI();

            Vector2 targetPosition = Main.player[NPC.target].position;
            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;

            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            if (attackTimer == 200)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.FlameBurst, 0f, -2.5f, 0, default, 1.7f);

                if (!Main.dedServ)
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);

                NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);

                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.FlameBurst, 0f, -2.5f, 0, default, 1.7f);
            }
            if (attackTimer >= 250 && attackTimer < 300)
            {
                frameNum = 1;

                attackDelay--;

                if (attackDelay < 0)
                {
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundID.DD2_BetsyFireballShot, NPC.position);

                    if (!Main.dedServ)
                    {
                        int[] i =
                        {
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ProjectileID.InfernoHostileBolt, NPC.damage / 3, 0),
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ProjectileID.InfernoHostileBolt, NPC.damage / 3, 0),
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ProjectileID.InfernoHostileBolt, NPC.damage / 3, 0),
                            Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ProjectileID.InfernoHostileBolt, NPC.damage / 3, 0)
                        };

                        for (int j = 0; j < i.Length; j++)
                        {
                            Main.projectile[i[j]].timeLeft = 300;
                            Main.projectile[i[j]].extraUpdates = 4;
                            Main.projectile[i[j]].tileCollide = false;
                        }
                    }

                    attackDelay = attackDelayMax;
                }
            }
            if (attackTimer == 350)
            {
                frameNum = 0;

                attackTimer = 0;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameNum * frameHeight;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {
                for (int k = 0; k < 5; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 2.5f, -2.5f, 0, default, 1.7f);
            }
            else
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.Torch, 0, -1f, 0, default(Color), 1f);
            }
        }
    }
}
