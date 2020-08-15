using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items
{
    class BloodLocket : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Activates Hell Mode" +
                "\nCan be toggled on and off" +
                "\nCan only be used in Expert only" +
                "\nGives Eternal Mod Bosses Increaced Health, Defense, and Damage" +
                "\nSome Eternal Mod Bosses will have diffrent attack patterns" +
                "\nBosses will drop unique items, accesories, and weapons exclusive to this difficulty" +
                "\nMakes some Mini-Bosses EXTREME (NYI)" +
                "\n'Enjoy your pain and suffering, kid...'");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 38;
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
                Main.NewText("Hell Mode is Active, enjoy the fun!", 215, 0, 215);
            }
            else if (EternalWorld.hellMode == true)
            {
                Main.PlaySound(SoundID.ForceRoar, player.position, 0);
                EternalWorld.hellMode = false;
                Main.NewText("Hell Mode is no longer Active, not enough fun for you!", 215, 0, 215);
            }

            return Main.expertMode;
        }

    }
}
