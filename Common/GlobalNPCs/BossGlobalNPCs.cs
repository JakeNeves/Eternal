﻿using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalNPCs
{
    public class BossGlobalNPCs : GlobalNPC
    {

        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            if (DifficultySystem.hellMode && ModContent.GetInstance<CommonConfig>().hellModeVanillaBosses)
            {
                switch (npc.type)
                {
                    //Pre-Hardmode
                    case NPCID.KingSlime:
                        npc.damage = 98;
                        npc.lifeMax = 4000;
                        break;
                    case NPCID.EyeofCthulhu:
                        npc.damage = 50;
                        npc.lifeMax = 5000;
                        break;
                    #region Eater of worlds
                    case NPCID.EaterofWorldsHead:
                        npc.lifeMax = 300;
                        npc.damage = 80;
                        npc.defense = 8;
                        break;
                    case NPCID.EaterofWorldsBody:
                        npc.lifeMax = 300;
                        npc.damage = 40;
                        npc.defense = 10;
                        break;
                    case NPCID.EaterofWorldsTail:
                        npc.lifeMax = 300;
                        npc.damage = 48;
                        npc.defense = 12;
                        break;
                    #endregion
                    case NPCID.BrainofCthulhu:
                        npc.damage = 86;
                        npc.lifeMax = 7000;
                        break;
                    case NPCID.QueenBee:
                        npc.damage = 90;
                        npc.lifeMax = 7000;
                        break;
                    #region Skeletron
                    case NPCID.SkeletronHead:
                        npc.damage = 110;
                        npc.lifeMax = 12000;
                        break;
                    case NPCID.SkeletronHand:
                        npc.damage = 70;
                        npc.lifeMax = 2000;
                        break;
                    #endregion
                    case NPCID.Deerclops:
                        npc.lifeMax = 30344;
                        break;
                    #region WoF
                    case NPCID.WallofFlesh:
                        npc.damage = 230;
                        npc.lifeMax = 20000;
                        npc.defense = 20;
                        break;
                    case NPCID.WallofFleshEye:
                        npc.damage = 96;
                        npc.lifeMax = 20000;
                        npc.defense = 20;
                        break;
                    #endregion
                    //Hardmode
                    #region Mech Bosses
                    //Skeletron Prime
                    case NPCID.SkeletronPrime:
                        npc.damage = 300;
                        npc.lifeMax = 60000;
                        npc.defense = 50;
                        break;
                    case NPCID.PrimeCannon:
                        npc.damage = 248;
                        npc.lifeMax = 20000;
                        npc.defDefense = 40;
                        break;
                    case NPCID.PrimeSaw:
                        npc.damage = 248;
                        npc.lifeMax = 20000;
                        npc.defDefense = 40;
                        break;
                    case NPCID.PrimeVice:
                        npc.damage = 248;
                        npc.lifeMax = 20000;
                        npc.defDefense = 40;
                        break;
                    case NPCID.PrimeLaser:
                        npc.damage = 248;
                        npc.lifeMax = 20000;
                        npc.defDefense = 40;
                        break;
                    //Twins
                    case NPCID.Retinazer:
                        npc.damage = 144;
                        npc.lifeMax = 50000;
                        npc.defense = 16;
                        break;
                    case NPCID.Spazmatism:
                        npc.damage = 144;
                        npc.lifeMax = 50000;
                        npc.defense = 16;
                        break;
                    //Destroyer
                    case NPCID.TheDestroyer:
                        npc.damage = 840;
                        break;
                    case NPCID.TheDestroyerBody:
                        npc.damage = 280;
                        npc.defense = 36;
                        break;
                    case NPCID.TheDestroyerTail:
                        npc.damage = 204;
                        npc.defense = 48;
                        break;
                    #endregion
                    case NPCID.Plantera:
                        npc.damage = 200;
                        npc.lifeMax = 60000;
                        npc.defense = 48;
                        break;
                    case NPCID.DukeFishron:
                        npc.damage = 300;
                        npc.lifeMax = 80000;
                        npc.defense = 60;
                        break;
                    case NPCID.EmpressButterfly:
                        npc.lifeMax = 249900;
                        npc.defense = 70;
                        npc.damage = 360;
                        break;
                    case NPCID.QueenSlimeBoss:
                        npc.lifeMax = 73440;
                        npc.defense = 50;
                        npc.damage = 330;
                        break;
                    //Moon Lord
                    case NPCID.MoonLordCore:
                        npc.lifeMax = 100000;
                        npc.defense = 80;
                        break;
                    case NPCID.MoonLordHand:
                        npc.lifeMax = 50000;
                        npc.defense = 50;
                        npc.damage = 400;
                        break;
                    case NPCID.MoonLordHead:
                        npc.lifeMax = 90000;
                        npc.defense = 60;
                        npc.damage = 480;
                        break;
                }
            }
        }

    }
}