using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    public class BloodLocket : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death Charm");
            Tooltip.SetDefault("Activates Hell Mode" +
                "\nCan be toggled on and off" +
                "\nCan only be used in Expert only" +
                "\nGives Bosses Increaced Health, Defense, and Damage" +
                "\nSome Bosses will have diffrent and unique attack patterns" +
                "\nBosses will drop unique items, accesories, and weapons exclusive to this difficulty" +
                "\n'You're playing with chaos'");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useAnimation = 15;
            item.useTime = 15;
            item.rare = ItemRarityID.Expert;
            item.value = 0;
            item.expert = true;
        }

        public override bool CanUseItem(Player player)
        {
            if(EternalWorld.hellMode == false)
            {
                Main.PlaySound(SoundID.ForceRoar, player.position, 0);
                EternalWorld.hellMode = true;
                Main.NewText("Hell Mode is Active, enjoy the fun!", 210, 0, 220);
            }
            else if (EternalWorld.hellMode == true)
            {
                Main.PlaySound(SoundID.ForceRoar, player.position, 0);
                EternalWorld.hellMode = false;
                Main.NewText("Hell Mode is no longer Active, not enough fun for you!", 210, 0, 220);
            }

            return Main.expertMode;
        }

    }
}
