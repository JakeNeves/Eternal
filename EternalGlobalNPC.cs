using Eternal.Items.Materials;
using Eternal.Items.Materials.Elementalblights;
using Eternal.Items.Weapons.Hell;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.NPCs
{
    public class EternalGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public static bool hellModeDifficulty = EternalWorld.hellMode;

        #region Defbuffs
        public bool doomFire;
        public bool embericComustion;
        #endregion

        public override void ResetEffects(NPC npc)
        {
            doomFire = false;
            embericComustion = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (doomFire)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 20;
                if (damage < 2)
                {
                    damage = 20;
                }
            }
            else if (embericComustion)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 15;
                if (damage < 2)
                {
                    damage = 15;
                }
            }
        }

        public override void SetDefaults(NPC npc)
        {
            if (EternalWorld.downedCarmaniteScouter)
            {
            }
            if (EternalWorld.downedDunekeeper)
            {
            }
        }

        public override void NPCLoot(NPC npc)
        {
            #region Boss Triggers
            if (NPC.AnyNPCs(NPCID.EyeofCthulhu))
                if (!NPC.downedBoss1)
                    Main.NewText("A faint screech can be heard from deep below...", 224, 28, 7);

            if (NPC.AnyNPCs(NPCID.Skeleton))
            {
                if (!NPC.downedBoss3)
                    Main.NewText("The dark caves beneath the world go silent...", 224, 28, 7);
                if (hellModeDifficulty)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<SkeletronJawbone>());
                    if (!Main.LocalPlayer.HasItem(ModContent.ItemType<KnifeBlade>()) || !Main.LocalPlayer.HasItem(ModContent.ItemType<TheCleaver>()))
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CleaverHead>());
                    }
                }
            }

            if (NPC.AnyNPCs(NPCID.Plantera))
                if (!NPC.downedPlantBoss)
                    Main.NewText("A cold gust of wind blows from the tundra...", 7, 28, 224);

            if (NPC.AnyNPCs(NPCID.MoonLordCore))
            {
                if (!NPC.downedMoonlord)
                {
                    Main.NewText("The tundra freezes restless...", 7, 28, 224);
                    Main.NewText("A faint eterial hum can be heard from the shrine...", 48, 255, 179);
                    Main.NewText("A comet has landed and struck the world!", 0, 215, 215);
                    EternalWorld.DropComet();
                }
                else
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.NewText("A comet has landed and struck the world!", 0, 215, 215);
                        EternalWorld.DropComet();
                    }
                }
            }
            if (NPC.AnyNPCs(NPCID.WallofFlesh))
            {
                if (hellModeDifficulty)
                {
                    if (!Main.LocalPlayer.HasItem(ModContent.ItemType<KnifeBlade>()) || !Main.LocalPlayer.HasItem(ModContent.ItemType<TheKnife>()))
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<KnifeBlade>());
                    }
                }
            }
            #endregion

            Player player = Main.player[Main.myPlayer];

            #region Elemental Drops
            if (!Main.dayTime)
            {
                if (NPC.AnyNPCs(NPCID.PossessedArmor) || NPC.AnyNPCs(NPCID.Wraith) || NPC.AnyNPCs(NPCID.Werewolf))
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<DuskblightCrystal>(), Main.rand.Next(2, 8));
                    }
                }
            }

            if (Main.bloodMoon)
            {
                if (NPC.AnyNPCs(NPCID.BloodZombie) || NPC.AnyNPCs(NPCID.Drippler))
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CarnageblightCrystal>(), Main.rand.Next(1, 4));
                    }
                }
            }

            if (player.ZoneUnderworldHeight)
            {
                if (NPC.AnyNPCs(NPCID.Demon) || NPC.AnyNPCs(NPCID.Lavabat) || NPC.AnyNPCs(NPCID.Hellbat) || NPC.AnyNPCs(NPCID.LavaSlime) || NPC.AnyNPCs(NPCID.BoneSerpentHead) || NPC.AnyNPCs(NPCID.HellArmoredBones))
                {
                    if (Main.hardMode)
                    {
                        if (Main.rand.Next(3) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<InfernoblightCrystal>(), Main.rand.Next(2, 8));
                        }
                    }
                }
            }

            if (player.ZoneBeach)
            {
                if (NPC.AnyNPCs(NPCID.GreenJellyfish) || NPC.AnyNPCs(NPCID.BlueJellyfish) || NPC.AnyNPCs(NPCID.PinkJellyfish) || NPC.AnyNPCs(NPCID.Shark))
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<AquablightCrystal>(), Main.rand.Next(1, 4));
                    }
                }
            }

            if (player.ZoneSnow)
            {
                if (NPC.AnyNPCs(NPCID.IceGolem) || NPC.AnyNPCs(NPCID.IceElemental) || NPC.AnyNPCs(NPCID.IceBat) || NPC.AnyNPCs(NPCID.IceTortoise))
                {
                    if (EternalWorld.downedSubzeroElemental)
                    {
                        if (Main.rand.Next(3) == 0)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<FrostblightCrystal>(), Main.rand.Next(1, 4));
                        }
                    }
                }
            }

            if (player.ZoneJungle)
            {
                if (NPC.AnyNPCs(NPCID.GiantFlyingFox) || NPC.AnyNPCs(NPCID.GiantTortoise) || NPC.AnyNPCs(NPCID.JungleBat) || NPC.AnyNPCs(NPCID.Snatcher) || NPC.AnyNPCs(NPCID.ManEater) || NPC.AnyNPCs(NPCID.AngryTrapper) || NPC.AnyNPCs(NPCID.Hornet))
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<NatureblightCrystal>(), Main.rand.Next(1, 4));
                    }
                }
            }

            if (Main.raining)
            {
                if (NPC.AnyNPCs(NPCID.AngryNimbus))
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ThunderblightCrystal>(), Main.rand.Next(1, 4));
                    }
                }
            }
            #endregion
        }

        #region Hell Mode Vanilla Bosses
        public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
        {
            if (hellModeDifficulty && ModContent.GetInstance<EternalConfig>().hellModeVanillaBosses)
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
        #endregion

        public override void AI(NPC npc)
        {
            if (EternalWorld.downedCarmaniteScouter)
            {
            }
            if (EternalWorld.downedDunekeeper)
            {
            }
        }
    }
}
