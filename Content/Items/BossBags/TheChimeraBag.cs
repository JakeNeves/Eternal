using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Hell;
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
    public class TheChimeraBag : ModItem
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
            Item.rare = -12;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<GasBag>()));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RottenFangspear>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RottenMeathook>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Necrobow>(), 3));
        }
    }
}
