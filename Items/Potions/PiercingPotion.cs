using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Potions
{
    public class PiercingPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Your attacks inflict Red Fracture");
        }

        public override void SetDefaults()
		{
			item.width = 18;
			item.height = 28;
			item.consumable = true;
			item.maxStack = 30;
			item.useStyle = ItemUseStyleID.EatingUsing;
			item.useAnimation = 16;
			item.useTime = 16;
			item.useTurn = true;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(gold: 20);
			item.buffType = ModContent.BuffType<Buffs.Piercing>();
			item.buffTime = 5200;
			item.UseSound = SoundID.Item3;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddTile(TileID.Bottles);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddIngredient(ItemID.Daybloom, 4);
			recipe.AddIngredient(ItemID.CrimtaneOre, 2);
			recipe.AddIngredient(ItemID.TissueSample, 3);
			recipe.AddIngredient(ItemID.Blinkroot, 6);
			recipe.SetResult(this);
			recipe.AddRecipe();

			ModRecipe altrecipe = new ModRecipe(mod);
			altrecipe.AddTile(TileID.Bottles);
			altrecipe.AddIngredient(ItemID.BottledWater);
			altrecipe.AddIngredient(ItemID.Daybloom, 4);
			altrecipe.AddIngredient(ItemID.DemoniteOre, 2);
			altrecipe.AddIngredient(ItemID.ShadowScale, 3);
			altrecipe.AddIngredient(ItemID.Blinkroot, 6);
			altrecipe.SetResult(this);
			altrecipe.AddRecipe();
		}
	}
}
