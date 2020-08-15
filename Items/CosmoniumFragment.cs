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
    class CosmoniumFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cosmic energy pulses from this mere fragment of celestial creatures.");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(3, 16));
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.buyPrice(platinum: 1, gold: 25, silver: 50);
            item.rare = -12;
            item.maxStack = 999;
        }
    }
}
