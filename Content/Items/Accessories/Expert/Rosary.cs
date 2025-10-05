using Eternal.Content.Buffs.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Expert
{
    public class Rosary : ModItem
    {
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 38;
			Item.rare = ItemRarityID.Pink;
			Item.expert = true;
            Item.accessory = true;
            Item.value = Item.sellPrice(silver: 13, gold: 2);
            Item.buffType = ModContent.BuffType<Buffs.HolyMantle>();
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (!player.HasBuff(ModContent.BuffType<HolyMantleCooldown>()))
                player.AddBuff(Item.buffType, 2);
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Accessories;
        }
    }
}
