﻿using Eternal.Dusts;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs.Labrynth
{
    public class LesserArk : ModNPC
    {

        private Player player;
        private float speed;

        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 150;
            npc.damage = 20;
            npc.defense = 10;
            npc.knockBackResist = 0f;
            npc.width = 38;
            npc.height = 60;
            npc.value = Item.buyPrice(silver: 8);
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.DD2_DarkMageHealImpact;
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
            Target();
            Move(new Vector2(0, 0));
            RotateNPCToTarget();
        }

        private void Target()
        {
            npc.TargetClosest(true);
            player = Main.player[npc.target];
        }

        private float Magnitude(Vector2 mag)
        {
            return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
        }

        private void RotateNPCToTarget()
        {
            if (player == null) return;
            Vector2 direction = npc.Center - player.Center;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            npc.rotation = rotation + ((float)Math.PI * 0.5f);
        }

        private void Move(Vector2 offset)
        {
            speed = 2f;
            Vector2 moveTo = player.Center + offset;
            Vector2 move = moveTo - npc.Center;
            float magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            float turnResistance = 5f;
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            magnitude = Magnitude(move);
            if (magnitude > speed)
            {
                move *= speed / magnitude;
            }
            npc.velocity = move;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { ModContent.TileType<LabrynthStone>(), TileID.Grass, TileID.Dirt, TileID.Stone, TileID.Sand, TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneLabrynth && !NPC.downedMoonlord ? 2.09f : 0f;
            }
            return SpawnCondition.OverworldDay.Chance * 0.5f;
        }

    }
}