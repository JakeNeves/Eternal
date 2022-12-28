using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Dusts;
using Eternal.Content.Items.Armor;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Comet
{
    public class AstroidSmasher : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.lifeMax = 22000;
            NPC.damage = 120;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 80;
            NPC.height = 64;
            NPC.aiStyle = -1;
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

                new FlavorTextBestiaryInfoElement("These creatures pack quite a punch, they are srtong enough to break astroids into four.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            
            int[] cometTileArray = { ModContent.TileType<CometiteOre>(), TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

            float baseChance = SpawnCondition.Overworld.Chance;
            float multiplier = cometTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.5f : 1f;

            if (DownedBossSystem.downedCosmicApparition && ModContent.GetInstance<ZoneSystem>().zoneComet)
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
            Lighting.AddLight(NPC.position, 0.75f, 0f, 0.75f);
            NPC.rotation = NPC.velocity.X * 0.03f;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            NPC.spriteDirection = NPC.direction = NPC.Center.X < player.Center.X ? -1 : 1;
            if (player.dead || !player.active)
            {
                NPC.TargetClosest(false);
            }

            float speed = 12.5f;
            float acceleration = 0.10f;
            Vector2 vector2 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float xDir = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector2.X;
            float yDir = (float)(Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - 120) - vector2.Y;
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
            if (NPC.velocity.X < xDir)
            {
                NPC.velocity.X = NPC.velocity.X + acceleration;
                if (NPC.velocity.X < 0 && xDir > 0)
                    NPC.velocity.X = NPC.velocity.X + acceleration;
            }
            else if (NPC.velocity.X > xDir)
            {
                NPC.velocity.X = NPC.velocity.X - acceleration;
                if (NPC.velocity.X > 0 && xDir < 0)
                    NPC.velocity.X = NPC.velocity.X - acceleration;
            }
            if (NPC.velocity.Y < yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y + acceleration;
                if (NPC.velocity.Y < 0 && yDir > 0)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
            }
            else if (NPC.velocity.Y > yDir)
            {
                NPC.velocity.Y = NPC.velocity.Y - acceleration;
                if (NPC.velocity.Y > 0 && yDir < 0)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<ApparitionalMatter>(), 1, 12, 24));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Astragel>(), 1, 12, 24));
            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<InterstellarSingularity>(), 1, 12, 24));

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
