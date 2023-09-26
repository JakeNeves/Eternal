using Eternal.Common.Configurations;
using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Common.Systems;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Misc;
using Terraria.Audio;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Eternal.Common.Misc;

namespace Eternal.Common.GlobalNPCs
{
    public class BossGlobalNPCs : GlobalNPC
    {
        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            Player player = Main.player[Main.myPlayer];

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (npc.boss && ClientConfig.instance.playDefeatSound)
                {
                    if (npc.life <= 0)
                    {
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/BossDefeated"), player.position);
                    }
                }
            }
        }

        public override void AI(NPC npc)
        {
            if (ClientConfig.instance.bossBarExtras)
            {
                switch (npc.type)
                {
                    case NPCID.KingSlime:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server )
                        {
                            EternalBossBarOverlay.SetTracked("Mindless Tyrant, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.EyeofCthulhu:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Evil Presence of the Night Sky, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.EaterofWorldsHead:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Centipede Sabotuer of The Corruption, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.BrainofCthulhu:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Crimson, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.QueenBee:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Matriarch of The Hive, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.SkeletronHead:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Dungeon, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Deerclops:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of the Tundra, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.WallofFlesh:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Underworld, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.SkeletronPrime:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Mechanical Terror of The Cold Wind, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Retinazer:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Night Sky, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Spazmatism:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Night Sky, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.TheDestroyer:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Rising from The Ground, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Plantera:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Jungle, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.DukeFishron:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Rising from The Ocean, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.EmpressButterfly:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Guardian of The Hallowed, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.QueenSlimeBoss:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Gelutonous Sabotuer, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Golem:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Guardian of The Lihzard Temple, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.CultistBoss:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Dungeon, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.MoonLordCore:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server)
                        {
                            EternalBossBarOverlay.SetTracked("Celestial Deity of The Moon, ", npc, ModContent.Request<Texture2D>("Eternal/Assets/Textures/UI/EternalBossBarFrame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                }
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            if (DifficultySystem.hellMode && ServerConfig.instance.hellModeVanillaBosses)
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

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            HellModeDropCondition hellModeDrop = new HellModeDropCondition();

            switch (npc.type)
            {
                case NPCID.WallofFlesh:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<HolyCard>(), 1));
                    break;

                case NPCID.SkeletronHead:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<ShadowSkull>(), 1));
                    break;
            }
        }
    }
}
