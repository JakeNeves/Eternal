using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Common.GlobalItems
{
    public class BossBagGlobalItem : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch (item.type) {
                case ItemID.EyeOfCthulhuBossBag:
                        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<KnifeHandle>(), 24));
                    break;

                case ItemID.PlanteraBossBag:
                        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<KnifeBlade>(), 24));
                    break;
            }
        }
    }
}
