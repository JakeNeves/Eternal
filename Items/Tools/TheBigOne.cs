using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Tools
{
    class TheBigOne : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jaer Hend's Mighty Forest Felling Mega Axe of Eternity");
            Tooltip.SetDefault("A Very Big Axe\n'Sprited by Jaer Hend'\nName Inspired from Triactics' True Panidinian Mage-Hammer of Might\n[c/FC036B:Dedicated Content]\nDedicated to [c/038CFC:Jaer Hend/Jerrhead]");
        }

        public override void SetDefaults()
        {
            item.damage = 10000;
            item.melee = true;
            item.width = 170;
            item.height = 164;
            item.useTime = 50;
            item.useAnimation = 50;
            item.axe = 100;
            item.hammer = 200;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 0;
            item.value = Item.buyPrice(platinum: 1);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<StellarAlloy>(), 5);
            recipe.AddIngredient(ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemID.PossessedHatchet);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItemFrame(Player player)
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }

    }
}
