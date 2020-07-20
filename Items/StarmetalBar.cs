using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace Eternal.Items
{
    class StarmetalBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'A Cosmic Bar That is Very Sturdy and Durable...'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            item.width = 29;
            item.height = 21;
            item.value = Item.buyPrice(gold: 15);
            item.rare = 10;
            item.maxStack = 99;
        }
    }
}
