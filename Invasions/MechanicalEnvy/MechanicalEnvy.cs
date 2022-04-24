using System;
using System.Collections.Generic;
using Eternal.NPCs.Invasion.MechanicalEnvy;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Invasions.MechanicalEnvy
{
    public class MechanicalEnvy // Credits to Sin Costan (https://forums.terraria.org/index.php?threads/tutorial-custom-invasion.57771/)
    {
        public static int[] invaders =
        {
            ModContent.NPCType<MicroCuboidLaserTurret>()
        };

        public static void StartCustomInvasion()
        {
            if (Main.invasionType != 0 && Main.invasionSize == 0)
            {
                Main.invasionType = 0;
            }

            if (Main.invasionType == 0)
            {
                int numPlayers = 0;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && Main.player[i].statLifeMax >= 200)
                    {
                        numPlayers++;
                    }
                }
                if (numPlayers > 0)
                {
                    Main.invasionType = -1;
                    EternalWorld.mechanicalEnvyUp = true;
                    Main.invasionSize = 115 * numPlayers;
                    Main.invasionSizeStart = Main.invasionSize;
                    Main.invasionProgress = 0;
                    Main.invasionProgressIcon = 0 + 3;
                    Main.invasionProgressWave = 0;
                    Main.invasionProgressMax = Main.invasionSizeStart;
                    Main.invasionWarn = 3600;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.invasionX = 0.0;
                        return;
                    }
                    Main.invasionX = (double)Main.maxTilesX;
                }
            }
        }

        public static void CustomInvasionWarning()
        {
            string text = "";
            if (Main.invasionX == (double)Main.spawnTileX)
            {
                text = "Unknown machines can be heard rumbling in the distance!";
            }
            if (Main.invasionSize <= 0)
            {
                text = "The Machines have retreated!";
            }
            if (Main.netMode == 0)
            {
                Main.NewText(text, 175, 75, 255, false);
                return;
            }
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f, 0, 0, 0);
            }
        }

        public static void UpdateCustomInvasion()
        {
            if (EternalWorld.mechanicalEnvyUp)
            {
                if (Main.invasionSize <= 0)
                {
                    EternalWorld.mechanicalEnvyUp = false;
                    CustomInvasionWarning();
                    Main.invasionType = 0;
                    Main.invasionDelay = 0;
                }

                if (Main.invasionX == (double)Main.spawnTileX)
                {
                    return;
                }

                float moveRate = (float)Main.dayRate;

                if (Main.invasionX > (double)Main.spawnTileX)
                {
                    Main.invasionX -= (double)moveRate;

                    if (Main.invasionX <= (double)Main.spawnTileX)
                    {
                        Main.invasionX = (double)Main.spawnTileX;
                        CustomInvasionWarning();
                    }
                    else
                    {
                        Main.invasionWarn--;
                    }
                }
                else
                {
                    if (Main.invasionX < (double)Main.spawnTileX)
                    {
                        Main.invasionX += (double)moveRate;
                        if (Main.invasionX >= (double)Main.spawnTileX)
                        {
                            Main.invasionX = (double)Main.spawnTileX;
                            CustomInvasionWarning();
                        }
                        else
                        {
                            Main.invasionWarn--;
                        }
                    }
                }
            }
        }

        public static void CheckCustomInvasionProgress()
        {
            if (Main.invasionProgressMode != 2)
            {
                Main.invasionProgressNearInvasion = false;
                return;
            }

            bool flag = false;
            Player player = Main.player[Main.myPlayer];
            Rectangle rectangle = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth, Main.screenHeight);
            int num = 5000;
            int icon = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active)
                {
                    icon = 0;
                    int type = Main.npc[i].type;
                    for (int n = 0; n < invaders.Length; n++)
                    {
                        if (type == invaders[n])
                        {
                            Rectangle value = new Rectangle((int)(Main.npc[i].position.X + (float)(Main.npc[i].width / 2)) - num, (int)(Main.npc[i].position.Y + (float)(Main.npc[i].height / 2)) - num, num * 2, num * 2);
                            if (rectangle.Intersects(value))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
            }
            Main.invasionProgressNearInvasion = flag;
            int progressMax3 = 1;

            if (EternalWorld.mechanicalEnvyUp)
            {
                progressMax3 = Main.invasionSizeStart;
            }

            if (EternalWorld.mechanicalEnvyUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                Main.ReportInvasionProgress(Main.invasionSizeStart - Main.invasionSize, progressMax3, icon, 0);
            }

            foreach (Player p in Main.player)
            {
                NetMessage.SendData(MessageID.InvasionProgressReport, p.whoAmI, -1, null, Main.invasionSizeStart - Main.invasionSize, (float)Main.invasionSizeStart, (float)(Main.invasionType + 3), 0f, 0, 0, 0);
            }
        }
    }
}