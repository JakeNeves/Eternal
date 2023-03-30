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
    public class GobletofSinfulBlood : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblet of Sinful Blood");
            Tooltip.SetDefault("Activates Sinstorm Mode" +
                "\n[c/ED0249:This difficulty may require some experience in expert/master mode]" +
                "\nCan be toggled on and off" +
                "\nCan only be used in Hell mode" +
                "\nCannot be used when a Boss is Alive" +
                "\nGives Bosses Increaced Health and Damage" +
                "\nBosses will drop unique Items exclusive to this difficulty" +
                "\n'You have raised the bar far enough...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 34;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.rare = ModContent.RarityType<SinstormMode>();
            Item.value = 0;
        }

        public override bool? UseItem(Player player)
        {
            if (DifficultySystem.hellMode)
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
                    if (DifficultySystem.sinstormMode == false)
                    {
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/DifficultyActivate"), player.position);
                        DifficultySystem.sinstormMode = true;
                        Main.NewText("Sinstorm is active, you're playing with fire!", 236, 0, 100);
                        //Main.NewText("Hell Mode is Active, enjoy the fun!", 210, 0, 220);
                    }
                    else if (DifficultySystem.sinstormMode == true)
                    {
                        SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/DifficultyDeactivate"), player.position);
                        DifficultySystem.sinstormMode = false;
                        Main.NewText($"Sinstorm is no longer active, remain pure {player.name}...", 236, 0, 100);
                        //Main.NewText("Hell Mode is no longer Active, not enough fun for you!", 210, 0, 220);
                    }
                }
                else
                {
                    Main.NewText("Sinstorm can't be toggled at this time!", 236, 0, 100);
                    //Main.NewText("Hell Mode can't be toggled at this time!", 210, 0, 220);
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " tried to change the rules"), 10000, 1, false);
                }
            }
            else
            {
                Main.NewText("Sinstorm can't be toggled on an easier difficulty", 236, 0, 100);
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
