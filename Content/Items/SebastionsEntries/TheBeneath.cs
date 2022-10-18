using Eternal.Content.Rarities;
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
                "\nAfter hearing something stuck the world miles away from the research lab, I figured" +
                "\nit was just a meteorite. However, upon approaching it, I could see a purple light" +
                "\nwithin a vast distance, only to see that it was a comet that had fallen and strucked" +
                "\nthe plain of the earth... I went to take a closer look, only to be greeted by a ghostly" +
                "\nimage of someone with an unreconizable face, wandering this feld of where this comet has" +
                "\nstrucked. I had a sudden feeling it would haunt me in my sleep, I quickly ran back to the" +
                "\nlab before I noticed I was being followed by the entity, I didn't want to do anthing with it," +
                "\nsuddenly the entity started attacking after letting out an eerie shriek across the lab!" +
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
