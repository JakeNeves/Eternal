using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
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

namespace Eternal.Content.NPCs.Shrine
{
    public class FakeAoI : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fake Ark of Imperious");
        }

        public override void SetDefaults()
        {
            NPC.width = 86;
            NPC.height = 184;
            NPC.aiStyle = -1;
            NPC.defense = 10;
            NPC.lifeMax = 18000;
            NPC.value = Item.buyPrice(gold: 30, silver: 15);
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.DD2_DarkMageHealImpact;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = -1f;
            NPC.damage = 80;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("An ark attampting to mimic the look of the Ark of Imperious in attempt to ward off nearby intruders of the shrine.")
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)
            {
                for (int i = 0; i < 25; i++)
                {
                    Vector2 position = NPC.Center + Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360f / 25 * i)) * 15;
                    Dust dust = Dust.NewDustPerfect(NPC.position, DustID.JungleTorch);
                    dust.noGravity = true;
                    dust.velocity = Vector2.Normalize(position - NPC.Center) * 4;
                    dust.noLight = false;
                    dust.fadeIn = 1f;
                }
            }
            else
            {
                for (int k = 0; k < damage / NPC.lifeMax * 20.0; k++)
                {
                    Dust.NewDust(NPC.Center, NPC.width, NPC.height, DustID.GreenTorch, hitDirection, 0f, 0, default(Color), 0.7f);
                }
            }
        }

        public override void AI()
        {
            if (NPC.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.TargetClosest(true);
                NPC.ai[0] = 1f;
            }
            if (NPC.ai[1] != 3f && NPC.ai[1] != 2f)
            {
                SoundEngine.PlaySound(SoundID.DD2_BookStaffTwisterLoop, NPC.position);
                NPC.ai[1] = 2f;
            }
            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 2000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 2000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f)
                {
                    NPC.ai[1] = 3f;
                }
            }
            if (NPC.ai[1] == 2f)
            {
                NPC.rotation += NPC.direction * 0.03f;
                if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 250)
                {
                    NPC.velocity += Vector2.Normalize(Main.player[NPC.target].Center - NPC.Center) * new Vector2(3.75f, 1.75f);
                }

                NPC.velocity *= 0.98f;
                NPC.velocity.X = Utils.Clamp(NPC.velocity.X, -4, 4);
                NPC.velocity.Y = Utils.Clamp(NPC.velocity.Y, -2, 2);
            }
            else if (NPC.ai[1] == 3f)
            {
                NPC.velocity.Y = NPC.velocity.Y + 0.1f;
                if (NPC.velocity.Y < 0f)
                {
                    NPC.velocity.Y = NPC.velocity.Y * 0.95f;
                }
                NPC.velocity.X = NPC.velocity.X * 0.95f;
                if (NPC.timeLeft > 50)
                {
                    NPC.timeLeft = 50;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WeatheredPlating>(), 2, 2, 10));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawArkaniumDebris>(), 2, 12, 36));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WeatheredPlating>(), 2, 8, 14));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int[] shrineTileArray = { TileID.GrayBrick, TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

            float baseChance = SpawnCondition.Overworld.Chance;
            float multiplier = shrineTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.5f : 1f;
            if (NPC.downedMoonlord && ModContent.GetInstance<ZoneSystem>().zoneShrine)
            {
                return baseChance * multiplier;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
