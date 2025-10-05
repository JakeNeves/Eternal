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
    public class CosmicApparitionBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Bag (Cosmic Apparition)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            ItemID.Sets.BossBag[Type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ApparitionalMatter>(), minimumDropped: 30, maximumDropped: 60));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InterstellarScrapMetal>(), minimumDropped: 30, maximumDropped: 60));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarmetalBar>(), minimumDropped: 30, maximumDropped: 60));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Vexation>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Starfall>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ApparitionalDisk>(), 3));
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
    }
}
