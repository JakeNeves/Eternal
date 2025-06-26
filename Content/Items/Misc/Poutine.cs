using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class Poutine : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;

            ItemID.Sets.FoodParticleColors[Item.type] = new Color[1] {
                new Color(176, 123, 36)
            };
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 10;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 15);
            Item.buffType = ModContent.BuffType<Buffs.EmperorsPower>();
            Item.buffTime = 57600;
        }

        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.WellFed3, 57600);
        }
    }
}
