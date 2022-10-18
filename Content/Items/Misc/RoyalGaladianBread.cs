using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class RoyalGaladianBread : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Major improvements to all stats" +
                             "\n'Freshly baked by one of the emperor's favourite emissaries!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 16;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(gold: 12);
            Item.buffType = ModContent.BuffType<Buffs.EmperorsPower>();
            Item.buffTime = 57600;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.WellFed3, 57600);
        }
    }
}
