using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Tools
{
    class TritalodiumHammaxe : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Don't use this to skip pre-hardmode'");
        }

        public override void SetDefaults()
        {
            item.damage = 24;
            item.melee = true;
            item.width = 42;
            item.height = 44;
            item.useTime = 20;
            item.useAnimation = 20;
            item.hammer = 60;
            item.axe = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5;
            item.value = Item.sellPrice(silver: 25);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.Anvils);
            recipe.AddIngredient(ItemType<TritalodiumBar>(), 20);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
