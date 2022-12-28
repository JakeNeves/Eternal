using Eternal.Common.Systems;
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

namespace Eternal.Content.NPCs.Shrine
{
    public class GreaterArk : ModNPC
    {

        private Player player;
        private float speed;

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 42000;
            NPC.damage = 40;
            NPC.defense = 30;
            NPC.knockBackResist = 0f;
            NPC.width = 38;
            NPC.height = 60;
            NPC.value = Item.buyPrice(gold: 30);
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.DD2_ExplosiveTrapExplode;
            SpawnModBiomes = new int[1] { ModContent.GetInstance<Biomes.Shrine>().Type };
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("A stronger counterpart of the Lesser Ark, they will attempt to ram into you in act of impaling you in a brutal manner.")
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
            Target();
            Move(new Vector2(0, 0));
            RotateNPCToTarget();
        }

        private void Target()
        {
            NPC.TargetClosest(true);
            player = Main.player[NPC.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = NPC.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            NPC.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        private void Move(Vector2 offset)
        {
            speed = 8f;
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

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WeatheredPlating>(), 2, 2, 6));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RawArkaniumDebris>(), 2, 2, 12));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int[] shrineTileArray = { ModContent.TileType<ShrineBrick>(), TileID.Grass, TileID.Sand, TileID.Stone, TileID.SnowBlock, TileID.IceBlock, TileID.Dirt };

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
    }
}
