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
    class AncientDust : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Essance of ancient creatures who lived thousands of years ago...");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 14;
            item.value = Item.buyPrice(gold: 25, silver: 50);
            item.rare = 4;
            item.maxStack = 999;
        }
    }
}
