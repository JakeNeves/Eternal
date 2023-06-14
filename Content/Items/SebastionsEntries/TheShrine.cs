using Eternal.Content.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.SebastionsEntries
{
    public class TheShrine : ModItem
    {
        public override string Texture => "Eternal/Content/Items/SebastionsEntries/EntryLog";

        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("[c/2EA5BF:---Entry No. 2---]" +
                "\nAfter creating the Reconstructatorium, My team and I were sent out to find some" +
                "\nvaluable treasures, I stumbled upon a mysterious structure along with two of my" +
                "\nmembers, none of us had any clue on what this structure could be... Eventually, a" +
                "\nvoice from above, said to us... \"You are not to be within the area of our shrine," +
                "\nleave immidiately!\" Later, we were encountered by a group of what is supposedly" +
                "\nswords of various sizes! \"Arks, mind of you take care of our intruders, I don't" +
                "\naccept invites from strangers...\" At that moment, we fleed as fast as we can, yet" +
                "\nthe swords were chasing us for miles, eventually one of them went over us and nearly" +
                "\nimpaled me through the skull, luckly before even worse things happened, most of the" +
                "\nflying swords retreated, we used a C4 and detonated it, destroying the sword that got" +
                "\nstuck in the ground, nearly trying to brutally impal us, at least we were still alive." +
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
