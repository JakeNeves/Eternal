using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.BossBags
{
    public class CarminiteAmalgamationBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag (Carminite Amalgamation)");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            ItemID.Sets.BossBag[Type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = -12;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Carminite>(), minimumDropped: 24, maximumDropped: 36));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bloodtooth>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CarminiteBane>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CarminitePurgatory>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CarminiteRipperClaws>(), 3));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CarminiteDeadshot>(), 4));
        }
    }
}
