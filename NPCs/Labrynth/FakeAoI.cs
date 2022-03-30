using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using System.Linq;
using Eternal.Tiles;
using Eternal.Items.Materials;
using Eternal.Dusts;

namespace Eternal.NPCs.Labrynth
{
    public class FakeAoI : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fake Ark of Imperious");
        }

        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 180;
            npc.aiStyle = -1;
            npc.defense = 10;
            npc.lifeMax = 18000;
            npc.value = Item.buyPrice(gold: 30, silver: 15);
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.DD2_DarkMageHealImpact;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = -1f;
            npc.damage = 118;
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
            if (npc.ai[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.TargetClosest(true);
                npc.ai[0] = 1f;
            }
            if (npc.ai[1] != 3f && npc.ai[1] != 2f)
            {
                //Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                npc.ai[1] = 2f;
            }
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 2000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 2000f)
                {
                    npc.ai[1] = 3f;
                }
            }
            if (npc.ai[1] == 2f)
            {
                npc.rotation += npc.direction * 0.03f;
                if (Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 250)
                {
                    npc.velocity += Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * new Vector2(3.75f, 1.75f);
                }

                npc.velocity *= 0.98f;
                npc.velocity.X = Utils.Clamp(npc.velocity.X, -4, 4);
                npc.velocity.Y = Utils.Clamp(npc.velocity.Y, -2, 2);
            }
            else if (npc.ai[1] == 3f)
            {
                npc.velocity.Y = npc.velocity.Y + 0.1f;
                if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.95f;
                }
                npc.velocity.X = npc.velocity.X * 0.95f;
                if (npc.timeLeft > 50)
                {
                    npc.timeLeft = 50;
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<BrokenShrineSword>());
            }
            if (Main.rand.Next(6) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<WeatheredPlating>(), Main.rand.Next(6, 12));
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
            {
                int[] TileArray2 = { ModContent.TileType<LabrynthStone>(), TileID.Grass, TileID.Dirt, TileID.Stone, TileID.Sand, TileID.SnowBlock, TileID.IceBlock };
                return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && Main.LocalPlayer.GetModPlayer<EternalPlayer>().ZoneLabrynth && NPC.downedMoonlord ? 0.50f : 0f;
            }
            return 0f;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
    }
}
