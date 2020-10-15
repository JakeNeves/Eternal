using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;

namespace Eternal.Items.Accessories.Expert
{
    public class FrostKingsCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost King's Core");
            Tooltip.SetDefault("Grants Immunity to Chilled, Frostburn, and Frozen Debuffs"
                                           + "\nDash Effects, Attacks Inlfict Frostburn");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 30;
            item.value = Item.buyPrice(gold: 15);
            item.rare = ItemRarityID.Expert;
            item.expert = true;
        }
    }
}
