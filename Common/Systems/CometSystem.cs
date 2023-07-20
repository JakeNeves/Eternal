using Eternal.Content.Tiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.Systems
{
    /// <summary>
    /// A class to handle the Comet dropping, after defeating the Moon Lord
    /// </summary>
    public class CometSystem : ModSystem
    {
        /// <summary>
        /// Drop the Comet
        /// </summary>
        public static void DropComet()
        {
            bool flag = true;
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active)
                {
                    flag = false;
                    break;
                }
            }
            int num = 0;
            float num2 = Main.maxTilesX / 4200;
            int num3 = (int)(400f * num2);
            for (int j = 5; j < Main.maxTilesX - 5; j++)
            {
                int num4 = 5;
                while (num4 < Main.worldSurface)
                {
                    if (Main.tile[j, num4].HasTile && Main.tile[j, num4].TileType == (ushort)ModContent.TileType<CometiteOre>())
                    {
                        num++;
                        if (num > num3)
                        {
                            return;
                        }
                    }
                    num4++;
                }
            }
            float num5 = 600f;
            while (!flag)
            {
                float num6 = Main.maxTilesX * 0.08f;
                int num7 = Main.rand.Next(150, Main.maxTilesX - 150);
                while (num7 > Main.spawnTileX - num6 && num7 < Main.spawnTileX + num6)
                {
                    num7 = Main.rand.Next(150, Main.maxTilesX - 150);
                }
                int k = (int)(Main.worldSurface * 0.3);
                while (k < Main.maxTilesY)
                {
                    if (Main.tile[num7, k].HasTile && Main.tileSolid[Main.tile[num7, k].TileType])
                    {
                        int num8 = 0;
                        int num9 = 15;
                        for (int l = num7 - num9; l < num7 + num9; l++)
                        {
                            for (int m = k - num9; m < k + num9; m++)
                            {
                                if (WorldGen.SolidTile(l, m))
                                {
                                    num8++;
                                    if (Main.tile[l, m].TileType == 189 || Main.tile[l, m].TileType == 202)
                                    {
                                        num8 -= 100;
                                    }
                                }
                                else if (Main.tile[l, m].LiquidAmount > 0)
                                {
                                    num8--;
                                }
                            }
                        }
                        if (num8 < num5)
                        {
                            num5 -= 0.5f;
                            break;
                        }
                        flag = Comet(num7, k);
                        if (flag)
                        {
                        }
                        break;
                    }
                    k++;
                }
                if (num5 < 100f)
                {
                    return;
                }
            }
        }

        //also ripped this from tremor, credit to whoever wrote this originally...
        public static bool Comet(int i, int j)
        {
            if (i < 50 || i > Main.maxTilesX - 50)
            {
                return false;
            }
            if (j < 50 || j > Main.maxTilesY - 50)
            {
                return false;
            }
            int num = 35;
            Rectangle rectangle = new((i - num) * 16, (j - num) * 16, num * 2 * 16, num * 2 * 16);
            for (int k = 0; k < 255; k++)
            {
                if (Main.player[k].active)
                {
                    Rectangle value = new((int)(Main.player[k].position.X + Main.player[k].width / 2 - NPC.sWidth / 2 - NPC.safeRangeX), (int)(Main.player[k].position.Y + Main.player[k].height / 2 - NPC.sHeight / 2 - NPC.safeRangeY), NPC.sWidth + NPC.safeRangeX * 2, NPC.sHeight + NPC.safeRangeY * 2);
                    if (rectangle.Intersects(value))
                    {
                        return false;
                    }
                }
            }
            for (int l = 0; l < 200; l++)
            {
                if (Main.npc[l].active)
                {
                    Rectangle value2 = new((int)Main.npc[l].position.X, (int)Main.npc[l].position.Y, Main.npc[l].width, Main.npc[l].height);
                    if (rectangle.Intersects(value2))
                    {
                        return false;
                    }
                }
            }
            for (int m = i - num; m < i + num; m++)
            {
                for (int n = j - num; n < j + num; n++)
                {
                    if (Main.tile[m, n].HasTile && Main.tile[m, n].TileType == 21)
                    {
                        return false;
                    }
                }
            }
            num = WorldGen.genRand.Next(17, 23);
            for (int num2 = i - num; num2 < i + num; num2++)
            {
                for (int num3 = j - num; num3 < j + num; num3++)
                {
                    if (num3 > j + Main.rand.Next(-2, 3) - 5)
                    {
                        var tile1 = Main.tile[num2, num3];
                        float num4 = Math.Abs(i - num2);
                        float num5 = Math.Abs(j - num3);
                        float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                        if (num6 < num * 0.9 + Main.rand.Next(-4, 5))
                        {
                            if (!Main.tileSolid[Main.tile[num2, num3].TileType])
                            {
                                tile1.HasTile = false;
                            }
                            Main.tile[num2, num3].TileType = (ushort)ModContent.TileType<CometiteOre>();
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(8, 14);
            
            for (int num7 = i - num; num7 < i + num; num7++)
            {
                for (int num8 = j - num; num8 < j + num; num8++)
                {
                    if (num8 > j + Main.rand.Next(-2, 3) - 4)
                    {
                        var tile2 = Main.tile[num7, num8];
                        float num9 = Math.Abs(i - num7);
                        float num10 = Math.Abs(j - num8);
                        float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                        if (num11 < num * 0.8 + Main.rand.Next(-3, 4))
                        {
                            tile2.HasTile = false;
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(25, 35);
            for (int num12 = i - num; num12 < i + num; num12++)
            {
                for (int num13 = j - num; num13 < j + num; num13++)
                {
                    var tile3 = Main.tile[num12, num13];
                    float num14 = Math.Abs(i - num12);
                    float num15 = Math.Abs(j - num13);
                    float num16 = (float)Math.Sqrt(num14 * num14 + num15 * num15);
                    if (num16 < num * 0.7)
                    {
                        if (Main.tile[num12, num13].TileType == 5 || Main.tile[num12, num13].TileType == 32 || Main.tile[num12, num13].TileType == 352)
                        {
                            WorldGen.KillTile(num12, num13, false, false, false);
                        }
                        Main.tile[num12, num13].LiquidAmount = 0;
                    }
                    if (Main.tile[num12, num13].TileType == (ushort)ModContent.TileType<CometiteOre>())
                    {
                        if (!WorldGen.SolidTile(num12 - 1, num13) && !WorldGen.SolidTile(num12 + 1, num13) && !WorldGen.SolidTile(num12, num13 - 1) && !WorldGen.SolidTile(num12, num13 + 1))
                        {
                            tile3.HasTile = false;
                        }
                        else if ((Main.tile[num12, num13].IsHalfBlock || Main.tile[num12 - 1, num13].TopSlope) && !WorldGen.SolidTile(num12, num13 + 1))
                        {
                            tile3.HasTile = false;
                        }
                    }
                    WorldGen.SquareTileFrame(num12, num13, true);
                    WorldGen.SquareWallFrame(num12, num13, true);
                }
            }
            num = WorldGen.genRand.Next(23, 32);
            for (int num17 = i - num; num17 < i + num; num17++)
            {
                for (int num18 = j - num; num18 < j + num; num18++)
                {
                    if (num18 > j + WorldGen.genRand.Next(-3, 4) - 3 && Main.tile[num17, num18].HasTile && Main.rand.NextBool(10))
                    {
                        float num19 = Math.Abs(i - num17);
                        float num20 = Math.Abs(j - num18);
                        float num21 = (float)Math.Sqrt(num19 * num19 + num20 * num20);
                        if (num21 < num * 0.8)
                        {
                            if (Main.tile[num17, num18].TileType == 5 || Main.tile[num17, num18].TileType == 32 || Main.tile[num17, num18].TileType == 352)
                            {
                                WorldGen.KillTile(num17, num18, false, false, false);
                            }
                            Main.tile[num17, num18].TileType = (ushort)ModContent.TileType<CometiteOre>();
                            WorldGen.SquareTileFrame(num17, num18, true);
                        }
                    }
                }
            }
            num = WorldGen.genRand.Next(30, 38);
            for (int num22 = i - num; num22 < i + num; num22++)
            {
                for (int num23 = j - num; num23 < j + num; num23++)
                {
                    if (num23 > j + WorldGen.genRand.Next(-2, 3) && Main.tile[num22, num23].HasTile && Main.rand.NextBool(20))
                    {
                        float num24 = Math.Abs(i - num22);
                        float num25 = Math.Abs(j - num23);
                        float num26 = (float)Math.Sqrt(num24 * num24 + num25 * num25);
                        if (num26 < num * 0.85)
                        {
                            if (Main.tile[num22, num23].TileType == 5 || Main.tile[num22, num23].TileType == 32 || Main.tile[num22, num23].TileType == 352)
                            {
                                WorldGen.KillTile(num22, num23, false, false, false);
                            }
                            Main.tile[num22, num23].TileType = (ushort)ModContent.TileType<CometiteOre>();
                            WorldGen.SquareTileFrame(num22, num23, true);
                        }
                    }
                }
            }
            return true;
        }
    }
}