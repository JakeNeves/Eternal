using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Common.Systems;
using Terraria.Audio;
using Terraria.DataStructures;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Items.Materials;

namespace Eternal.Content.Items.Misc
{
    public class StarSpiral : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Star Spiral (NYI)");
            /* Tooltip.SetDefault("Unleashes a rift that shifts the world into an alternate reality" +
                "\n[c/ED0249:Warning]: Very powerful creatures lie within the rift!" +
                "\n'Just wait until I turn this world upside down!'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.rare = ModContent.RarityType<Teal>();
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 60;
            Item.useTime = 60;
        }

        public override bool? UseItem(Player player)
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
                if (!RiftSystem.isRiftOpen)
                {
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/RiftOpen"), player.position);
                    RiftSystem.isRiftOpen = true;
                    Main.NewText("The world shifts into an unstable realm!", 200, 100, 200);
                }
                else if (RiftSystem.isRiftOpen)
                {
                    SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/RiftClose"), player.position);
                    RiftSystem.isRiftOpen = false;
                    Main.NewText("The world shifts back into it's stablized tranquil state!", 200, 100, 200);
                }
            }
            else
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/StarSpiralKill"), player.position);
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "'s body became disfigured trying to close the rift,"), 10000, 1, false);
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 4)
                .AddIngredient(ModContent.ItemType<InterstellarScrapMetal>(), 4)
                .AddIngredient(ModContent.ItemType<Astragel>(), 4)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 4)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
