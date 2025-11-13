using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class ReallyLongBread : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[1] {
                new Color(124, 89, 64)
            };
        }

        public override void SetDefaults()
        {
            Item.width = 192;
            Item.height = 192;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 64;
            Item.useTime = 64;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item2;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 12);
            Item.buffType = BuffID.WellFed3;
            Item.buffTime = 57600;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.WellFed3, 57600);
        }

        public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
        {
            itemGroup = ContentSamples.CreativeHelper.ItemGroup.Food;
        }
    }
}
