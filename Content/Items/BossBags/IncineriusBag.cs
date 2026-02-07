using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Magic;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.BossBags
{
    public class IncineriusBag : ModItem
    {
        public override void SetStaticDefaults()
        {
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagmaniacHeart>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<IgneoforgedEdge>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Incinerator>(), 3));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernalDuplex>(), 4));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CanadianResistance>(), 10));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MagmaticAlloy>(), minimumDropped: 12, maximumDropped: 16));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InfernalAshes>(), minimumDropped: 24, maximumDropped: 32));
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossBags;
        }
    }
}
