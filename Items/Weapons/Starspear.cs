using Eternal.Projectiles;
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
			Tooltip.SetDefault("An example spear");
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

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[item.shoot] < 1;
		}

	}
}
