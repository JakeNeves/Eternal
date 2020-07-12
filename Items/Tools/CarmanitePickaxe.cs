using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Tools
{
    class CarmanitePickaxe : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'It feels like clay...'");
        }

        public override void SetDefaults()
        {
            item.damage = 8;
            item.melee = true;
            item.width = 60;
            item.height = 60;
            item.useTime = 10;
            item.useAnimation = 15;
            item.pick = 60;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2;
            item.value = Item.buyPrice(gold: 1, silver: 10);
            item.rare = ItemRarityID.Green;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.WorkBenches);
            recipe.AddIngredient(ItemType<Carmanite>(), 75);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
