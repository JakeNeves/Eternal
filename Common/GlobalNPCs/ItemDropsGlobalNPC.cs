using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalNPCs
{
    public class ItemDropsGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            PostGlareDrop postGlareDrop = new PostGlareDrop();

            if (npc.type == NPCID.Harpy || npc.type == NPCID.WyvernHead)
                npcLoot.Add(ItemDropRule.ByCondition(postGlareDrop, ModContent.ItemType<EssenceofFlight>(), 4, 1));
        }

        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            SoulOfBlight soulofBlightDropCondition = new SoulOfBlight();

            EssenceOfBlight essenceofBlightDropCondition = new EssenceOfBlight();
            EssenceOfLight essenceofLightDropCondition = new EssenceOfLight();
            EssenceOfNight essenceofNightDropCondition = new EssenceOfNight();

            globalLoot.Add(ItemDropRule.ByCondition(essenceofBlightDropCondition, ModContent.ItemType<EssenceofBlight>(), 4, 1));
            globalLoot.Add(ItemDropRule.ByCondition(essenceofLightDropCondition, ModContent.ItemType<EssenceofLight>(), 4, 1));
            globalLoot.Add(ItemDropRule.ByCondition(essenceofNightDropCondition, ModContent.ItemType<EssenceofNight>(), 4, 1));

            globalLoot.Add(ItemDropRule.ByCondition(soulofBlightDropCondition, ModContent.ItemType<SoulofBlight>(), 5, 1));
        }
    }
}
