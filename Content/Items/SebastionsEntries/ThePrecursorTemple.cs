using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.SebastionsEntries
{
    public class ThePrecursorTemple : ModItem
    {
        public override string Texture => "Eternal/Content/Items/SebastionsEntries/EntryLog";

        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("[c/2EA5BF:---Entry No. 3---]" +
                "\nOur team has decided to explore the caves of this world, they do hold quite" +
                "\nthe potential wonders of what the world has to offer uppon exploration, we have" +
                "\ndiscovered what could be an ancient civilization, inventions and other strange" +
                "\nmachinery made from crude and rudementary materials, yet most of these however" +
                "\nstood the test of time. It has come to my conclusion that there could be some" +
                "\nsort of autonomus gimmicks this place could have that we may still use today..." +
                "\n-Dr. Sebastion Kox"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 2));
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.rare = ItemRarityID.Blue;
        }
    }
}
