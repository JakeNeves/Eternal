using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Eternal.Content.Items.Misc
{
    public class StarSpiral : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Spiral (NYI)");
            /* Tooltip.SetDefault("Unleashes a rift that shifts the world into an alternate reality" +
                "\n[c/ED0249:Warning]: Very powerful creatures lie within the rift!" +
                "\n'Just wait until I turn this world upside down!'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.rare = ModContent.RarityType<Teal>();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 5;
            Item.useTime = 5;
        }
    }
}
