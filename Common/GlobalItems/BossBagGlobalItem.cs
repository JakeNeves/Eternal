using Eternal.Common.Configurations;
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
                    if (ServerConfig.instance.update14)
                        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<KnifeHandle14>(), 24));
                    break;

                case ItemID.PlanteraBossBag:
                    if (ServerConfig.instance.update14)
                        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<KnifeBlade14>(), 24));
                    break;
            }
        }
    }
}
