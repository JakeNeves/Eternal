using Eternal.Content.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.SebastionsEntries
{
    public class TheNanoforge : ModItem
    {
        public override string Texture => "Eternal/Content/Items/SebastionsEntries/EntryLog";

        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("[c/2EA5BF:---Entry No. 1---]" +
                "\nMy first ever successful reconstructing matrix, it's really impressive that" +
                "\nsuch machine like this can be used to create almost everything in an instant." +
                "\nI have yet, to see how I can go beyond the limits of the Reconstructatorium." +
                "\nBy creating some of the most otherworldly technology with a machine like this," +
                "\nI can almost do anything I want! However, only time will tell..." +
                "\n-Dr. Sebastion Kox"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 2));
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Turquoise>();
        }
    }
}
