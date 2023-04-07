using Eternal.Common.Systems;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class DeathCharm : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Charm of Death");
            /* Tooltip.SetDefault("Activates Hell Mode" +
                "\n[c/ED0249:This difficulty may require some experience in expert/master mode]" +
                "\nCan be toggled on and off" +
                "\nCan only be used in Expert and Master mode" +
                "\nCannot be used when a Boss is Alive" +
                "\nGives Bosses Increaced Health, Defense, and Damage" +
                "\nSome Bosses will have diffrent and unique attack patterns" +
                "\nBosses will drop unique Items, accesories, and weapons exclusive to this difficulty" +
                "\n'You're playing with chaos'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.value = 0;
        }

        public override bool? UseItem(Player player)
        {
            if (Main.expertMode || Main.masterMode)
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
                    if (DifficultySystem.hellMode == false)
                    {
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/DifficultyActivate"), player.position);
                        DifficultySystem.hellMode = true;
                        Main.NewText("Hell Mode is Active, witness the unspeakable menace!", 236, 0, 100);
                        //Main.NewText("Hell Mode is Active, enjoy the fun!", 210, 0, 220);
                    }
                    else if (DifficultySystem.hellMode == true)
                    {
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/DifficultyDeactivate"), player.position);
                        DifficultySystem.hellMode = false;
                        DifficultySystem.sinstormMode = false;
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
            }
            else
            {
                Main.NewText("Hell Mode can't be toggled on an easier difficulty", 236, 0, 100);
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " tried to change the rules"), 10000, 1, false);
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(TileID.DemonAltar);
        }
    }
}
