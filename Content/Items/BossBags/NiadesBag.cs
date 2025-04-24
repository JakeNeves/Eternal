using Eternal.Common.Systems;
using Eternal.Content.Items.Accessories.Expert;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Hell;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.BossBags
{
    public class NiadesBag : ModItem
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
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Rosary>()));
        }
    }
}
