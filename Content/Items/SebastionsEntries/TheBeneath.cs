using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.SebastionsEntries
{
    public class TheBeneath : ModItem
    {
        public override string Texture => "Eternal/Content/Items/SebastionsEntries/EntryLog";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/2EA5BF:---Entry No. 4---]" +
                "\nUppon exploring the deepest depths of this world's caverns, we noticed" +
                "\nsome black rock that is as durable as the materials used to make those" +
                "\nmachines as discovered in the Precursor Temple, however this hardened" +
                "\nrock is much tougher than regular stone, uppon deeper exploration into" +
                "\nsuch dark cave, all the lights went out followed by some thick fog, not" +
                "\neven something simple as torches will work, our team and I were left with" +
                "\nnothing but anxiety slowly getting to us, it's horrendous, we had to make" +
                "\nour escape as fast as we could, otherwise we could be consumed by what could" +
                "\nbe a potential monster that lurks in the darkness of this cave." +
                "\n-Dr. Sebastion Kox");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 2));
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 28;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
