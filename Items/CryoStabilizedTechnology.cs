using Terraria.ModLoader;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework;

namespace Eternal.Items
{
    class CryoStabilizedTechnology : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Sebastian's Well Constructed and Well Researched Core of Ice'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 14));
        }

        public override void SetDefaults()
        {
            item.width = 27;
            item.height = 37;
            item.value = Item.buyPrice(gold: 3, silver: 20);
            item.rare = ItemRarityID.Purple;
            item.maxStack = 999;
        }

    }
}
