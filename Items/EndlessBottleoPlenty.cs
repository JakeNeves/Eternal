using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Eternal.Items
{
    public class EndlessBottleoPlenty : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless Bottle o' Plenty");
            Tooltip.SetDefault("Reusable Healing Potion\nHas a 25 Second Cooldown\n'Now you can never run out of Health Potions...'");
        }

        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.rare = ItemRarityID.Red;
            item.width = 20;
            item.height = 28;
            item.value = Item.sellPrice(platinum: 5);
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useTurn = true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.FindBuffIndex(BuffID.PotionSickness) == -1)
            {
                return true;
            }
            return false;
        }

        public override bool UseItem(Player player)
        {
            if(player.FindBuffIndex(BuffID.PotionSickness) == -1)
            {
                player.AddBuff(BuffID.PotionSickness, 1500);
                player.lifeRegen = 500;
                player.releaseQuickHeal = true;
            }
            return false;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(5, 35, 215);
        }

    }
}
