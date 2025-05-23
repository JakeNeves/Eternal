﻿using Eternal.Common.Players;
using Eternal.Content.Buffs;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class HolyCard : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.UseSound = SoundID.Item4;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ModContent.RarityType<HellMode>();
            Item.buffType = ModContent.BuffType<HolyMantle>();
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (!player.GetModPlayer<BuffSystem>().holyMantle)
                {
                    player.AddBuff(Item.buffType, 2);
                }
            }

            return true;
        }
    }
}
