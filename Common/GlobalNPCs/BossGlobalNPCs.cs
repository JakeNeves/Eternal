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

        public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)
        {
            if (DifficultySystem.hellMode && npc.boss)
            {
                npc.damage = (int)(npc.damage * (balance * 1.05f) * bossAdjustment);
                npc.lifeMax = (int)(npc.lifeMax * (balance * 1.05f) * bossAdjustment);
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
            HellModeCommonDrop hellModeDrop = new HellModeCommonDrop();

            switch (npc.type)
            {
                case NPCID.EyeofCthulhu:
                    notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<KnifeHandle>(), 12));
                    break;

                case NPCID.WallofFlesh:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<HolyCard>(), 1));
                    break;

                case NPCID.SkeletronHead:
                    npcLoot.Add(ItemDropRule.ByCondition(hellModeDrop, ModContent.ItemType<ShadowSkull>(), 1));
                    break;

                case NPCID.Plantera:
                    notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<KnifeBlade>(), 12));
                    break;

                case NPCID.MoonLordCore:
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ConsumableCometFaller>(), 1, 1));
                    break;
            }
        }
    }
}
