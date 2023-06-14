using Eternal.Content.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.SebastionsEntries
{
    public class TheNeoxMechs : ModItem
    {
        public override string Texture => "Eternal/Content/Items/SebastionsEntries/EntryLog";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The N30X Mechs");

            /* Tooltip.SetDefault("[c/2EA5BF:---Entry No. 12---]" +
                "\nThey have mysteriously gone rouge, my creations!" +
                "\nThe prototypes have destroyed nearly half of the facility." +
                "\nWe have managed to evacuate before nearly risking our very" +
                "\nlives to our unspeakable, yet undesireable death! If our highness" +
                "\nhears about the wreckage of the facility, he could revoke our research permits" +
                "\nfor 6 months due to unsafe percautions of my creations... They must be destroyed" +
                "\nas soon as possible, whatever has possession of these creations, should immediately" +
                "\nbe punished for their reign of chaotic destruction of our facility." +
                "\n-Dr. Sebastion Kox"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 2));
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Magenta>();
        }
    }
}
