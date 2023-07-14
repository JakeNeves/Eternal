using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Tundra
{
    public class IceRoller : ModNPC
    {
        int attackTimer;

        private Player player;
        float speed;

        public override void SetDefaults()
        {
            NPC.width = 68;
            NPC.height = 68;
            NPC.lifeMax = 1500;
            NPC.defense = 20;
            NPC.damage = 48;
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frozen] = true;
            NPC.buffImmune[BuffID.Chilled] = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.Shatter;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,

                new FlavorTextBestiaryInfoElement("Soaring through the tundra, these constructs are made from pure ice, cristalized on a boulder.")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            var entitySource = NPC.GetSource_Death();

            if (NPC.life <= 0)
            {
                int gore1 = Mod.Find<ModGore>("IceRollerChunk").Type;
                int gore2 = Mod.Find<ModGore>("IceRollerCore").Type;

                for (int i = 0; i < 4; i++)
                    Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore1);
                Gore.NewGore(entitySource, NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), gore2);
            }
            else
            {
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Ice);
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            int[] snowTileArray = { TileID.SnowBlock, TileID.IceBlock, ModContent.TileType<GalaciteOre>() };

            float baseChance = SpawnCondition.Overworld.Chance;
            float multiplier = snowTileArray.Contains(Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].TileType) ? 0.05f : 1f;

            Player player = Main.player[Main.myPlayer];

            if (DownedBossSystem.downedCosmicApparition && player.ZoneSnow)
            {
                return baseChance * multiplier;
            }
            else
            {
                return SpawnCondition.Overworld.Chance * 0f;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            PostCosmicApparitionDropCondition postCosmicApparitionDrop = new PostCosmicApparitionDropCondition();

            npcLoot.Add(ItemDropRule.ByCondition(postCosmicApparitionDrop, ModContent.ItemType<Items.Placeable.GalaciteOre>(), 1, 6, 8));
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            var entitySource = NPC.GetSource_FromAI();

            Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
            direction.Normalize();
            direction.X *= 8.5f;
            direction.Y *= 8.5f;

            NPC.spriteDirection = NPC.direction;
            NPC.rotation += NPC.velocity.X * 0.1f;

            attackTimer++;
            if (attackTimer == 125 || attackTimer == 150 || attackTimer == 175)
            {
                float A = (float)Main.rand.Next(-200, 200) * 0.01f;
                float B = (float)Main.rand.Next(-200, 200) * 0.01f;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                    Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, direction.X + A, direction.Y + B, ProjectileID.FrostBlastHostile, NPC.damage, 1, Main.myPlayer, 0, 0);
            }
            else if (attackTimer == 250)
            {
                attackTimer = 0;
            }
        }
    }
}
