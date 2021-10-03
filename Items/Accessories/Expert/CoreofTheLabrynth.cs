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
    public class CoreofTheLabrynth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core of the Shrine");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 4));
        }

        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 62;
            item.value = Item.buyPrice(platinum: 15);
            item.rare = ItemRarityID.Expert;
            item.expert = true;
        }
    }
}
