using Eternal.Projectiles;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons
{
    class Starspear : ModItem
    {

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A Starsharp Spear");
		}

		public override void SetDefaults()
		{
			item.damage = 590;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 18;
			item.useTime = 24;
			item.shootSpeed = 4.5f;
			item.knockBack = 65f;
			item.width = 76;
			item.height = 76;
			item.rare = ItemRarityID.Red;
			item.value = Item.sellPrice(gold: 10);

			item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;

			item.UseSound = SoundID.Item1;
			item.shoot = ProjectileType<StarspearProjectile>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddTile(TileType<Starforge>());
			recipe.AddIngredient(ItemType<InterstellarSingularity>(), 5);
			recipe.AddIngredient(ItemType<StarmetalBar>(), 4);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1;
		}

	}
}
