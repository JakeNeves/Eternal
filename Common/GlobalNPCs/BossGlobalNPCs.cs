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
using Eternal.Common.Misc;
using Eternal.Content.BossBarStyles;
using Eternal.Content.Items.Debug;

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
            if (ClientConfig.instance.bossBarExtras && npc.boss)
            {
                switch (npc.type)
                {
                    case NPCID.KingSlime:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Gelatinous Tyrant", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.EyeofCthulhu:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Evil Presence of the Night Sky", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.EaterofWorldsHead:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Centipede Sabotuer of The Corruption", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.BrainofCthulhu:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Crimson, ", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.QueenBee:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Matriarch of The Hive", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.SkeletronHead:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Dungeon", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Deerclops:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of the Tundra", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.WallofFlesh:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Underworld", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.SkeletronPrime:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Mechanical Terror of The Cold Wind", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Retinazer:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Night Sky", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Spazmatism:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Night Sky", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.TheDestroyer:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Rising from The Ground", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Plantera:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Jungle", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.DukeFishron:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Rising from The Ocean", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.EmpressButterfly:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Guardian of The Hallowed", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.QueenSlimeBoss:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Hallowed", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.Golem:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Guardian of The Lihzard Temple", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.CultistBoss:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Scourge of The Dungeon", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    case NPCID.MoonLordCore:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("Celestial Deity of The Moon", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                    default:
                        if (!EternalBossBarOverlay.visible && Main.netMode != NetmodeID.Server && BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
                        {
                            EternalBossBarOverlay.SetTracked("", npc);
                            EternalBossBarOverlay.visible = true;
                        }
                        break;
                }
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
        {
            if (DifficultySystem.hellMode && ServerConfig.instance.hellModeVanillaBosses && npc.boss)
                bossAdjustment *= 2.5f;
            else if (DifficultySystem.sinstormMode && ServerConfig.instance.hellModeVanillaBosses && npc.boss)
                bossAdjustment *= 4f;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            HellModeDropCondition hellModeDrop = new HellModeDropCondition();

            switch (npc.type)
            {
                case NPCID.EyeofCthulhu:
                    if (!ServerConfig.instance.update14)
                        npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<KnifeHandle>(), 1));
                    break;

                case NPCID.WallofFlesh:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<HolyCard>(), 1));
                    break;

                case NPCID.SkeletronHead:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<ShadowSkull>(), 1));
                    break;

                case NPCID.Plantera:
                    if (!ServerConfig.instance.update14)
                        npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<KnifeBlade>(), 1));
                    break;

                case NPCID.MoonLordCore:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ConsumableCometFaller>(), 1, 1));
                    break;
            }
        }
    }
}
