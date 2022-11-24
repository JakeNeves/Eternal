using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Ranged;
using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Comet
{
    public class Starprobe : ModNPC
    {
        private Player player;

        int attackTimer = 0;
        int teleportTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starprobe");
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 1100;
            NPC.damage = 90;
            NPC.defense = 15;
            NPC.knockBackResist = -1f;
            NPC.width = 32;
            NPC.height = 26;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.DD2_SkeletonHurt;
            NPC.DeathSound = SoundID.NPCDeath5;
            NPC.value = Item.sellPrice(gold: 26, silver: 15);
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Comet>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("When the comet landed onto the surface of this world, these little probes started to unleash upon the surrounding area where the comet landed")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            
            int[] cometTileArray = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

            float baseChance = SpawnCondition.Overworld.Chance;
            float multiplier = cometTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.5f : 1f;

            if (ModContent.GetInstance<ZoneSystem>().zoneComet)
            {
                return baseChance * multiplier;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override void AI()
        {
            Vector2 targetPosition = Main.player[NPC.target].position;

            if (DownedBossSystem.downedCosmicApparition)
            {
                teleportTimer++;

                if (teleportTimer > 250)
                {
                    SoundEngine.PlaySound(SoundID.Item8, NPC.position);
                    NPC.position.X = targetPosition.X + Main.rand.Next(-400, 400);
                    NPC.position.Y = targetPosition.Y + Main.rand.Next(-400, 400);
                    for (int k = 0; k < 10; k++)
                    {
                        Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.DemonTorch, NPC.oldVelocity.X * 0.5f, NPC.oldVelocity.Y * 0.5f);
                    }
                    teleportTimer = 0;
                }
            }

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);

            Target();
            DespawnHandler();
            RotateNPCToTarget();

            attackTimer++;

            if (attackTimer == 200)
            {
                    float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                    float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.PinkLaser, NPC.damage, 1, Main.myPlayer, 0, 0);

                attackTimer = 0;
            }
        }

        private void Target()
        {
            player = Main.player[NPC.target];
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = NPC.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            NPC.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        private void DespawnHandler()
        {
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, -10f);
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<ApparitionalMatter>(), 1, 12, 24));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Astragel>(), 1, 12, 24));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<InterstellarSingularity>(), 1, 12, 24));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Meganovae>(), 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornMask>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHelmet>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHat>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornHeadgear>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornScalePlate>(), 12));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientStarbornGreaves>(), 12));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<CosmicSpirit>(), hitDirection, -1f, 0, default(Color), 1f);
            }
            else
            {
                for (int k = 0; k < damage / NPC.lifeMax * 50; k++)
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch, 2.5f * hitDirection, -2.5f, 0, default, 1.7f);
            }
        }
    }
}
