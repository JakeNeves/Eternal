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
using Eternal.Content.Items.Debug;
using Eternal.Content.Items.Weapons.Melee;

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

        public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
        {
            if (DifficultySystem.hellMode && ServerConfig.instance.hellModeVanillaBosses && npc.boss)
            {
                npc.damage *= 4;
                npc.lifeMax *= 4;
                npc.defense *= 3;
            }
            else if (DifficultySystem.sinstormMode && ServerConfig.instance.hellModeVanillaBosses && npc.boss)
            {
                npc.damage *= 5;
                npc.lifeMax *= 4;
                npc.defense *= 4;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            HellModeDropCondition hellModeDrop = new HellModeDropCondition();

            switch (npc.type)
            {
                case NPCID.EyeofCthulhu:
                    notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<KnifeHandle>(), 24));
                    break;

                case NPCID.WallofFlesh:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<HolyCard>(), 1));
                    break;

                case NPCID.SkeletronHead:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<ShadowSkull>(), 1));
                    break;

                case NPCID.Plantera:
                    notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<KnifeBlade>(), 24));
                    break;

                case NPCID.MoonLordCore:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ConsumableCometFaller>(), 1, 1));
                    break;
            }
        }
    }
}
