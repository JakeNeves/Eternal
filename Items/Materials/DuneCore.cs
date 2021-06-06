using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace Eternal.Items.Materials
{
    public class DuneCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(4, 4));
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 46;
            item.value = Item.buyPrice(silver: 50);
            item.rare = ItemRarityID.Orange;
            item.maxStack = 999;
        }
    }
}
