using Terraria;
using Terraria.DataStructures;
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
                "\nCannot be used when a Boss is Alive" +
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

        /*public override bool CanUseItem(Player player)
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
        }*/

        public override bool UseItem(Player player)
        {
            bool bossActive = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                {
                    bossActive = true;
                    break;
                }
            }

            if (!bossActive)
            {
                if (EternalWorld.hellMode == false)
                {
                    Main.PlaySound(SoundID.ForceRoar, player.position, 0);
                    EternalWorld.hellMode = true;
                    Main.NewText("Hell Mode is Active, witness the unspeakable menace!", 236, 0, 100);
                    //Main.NewText("Hell Mode is Active, enjoy the fun!", 210, 0, 220);
                }
                else if (EternalWorld.hellMode == true)
                {
                    Main.PlaySound(SoundID.ForceRoar, player.position, 0);
                    EternalWorld.hellMode = false;
                    Main.NewText("Hell Mode is no longer Active, breathe easy...", 236, 0, 100);
                    //Main.NewText("Hell Mode is no longer Active, not enough fun for you!", 210, 0, 220);
                }
            }
            else
            {
                Main.NewText("Hell Mode can't be toggled at this time!", 236, 0, 100);
                //Main.NewText("Hell Mode can't be toggled at this time!", 210, 0, 220);
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " tried to change the rules"), 10000, 1, false);
            }

            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
