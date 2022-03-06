using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class Starspear : ModItem
    {

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("<right> to throw and leave a trail of bombs" +
							 "\n'A Starsharp Spear'");
		}

		public override void SetDefaults()
		{
			item.damage = 590;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 18;
			item.useTime = 24;
			item.shootSpeed = 4.5f;
			item.knockBack = 4f;
			item.width = 66;
			item.height = 66;
			item.rare = ItemRarityID.Red;
			item.value = Item.sellPrice(gold: 10);

			item.melee = true;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.autoReuse = true;

			item.UseSound = SoundID.Item1;
			item.shoot = ModContent.ProjectileType<StarspearProjectile>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddTile(ModContent.TileType<Starforge>());
			recipe.AddIngredient(ModContent.ItemType<InterstellarSingularity>(), 5);
			recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 4);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			foreach (TooltipLine line2 in tooltips)
			{
				if (line2.mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.overrideColor = EternalColor.Teal;
				}
			}
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
            {
				item.useTime = 18;
				item.useAnimation = 18;
				item.shoot = ModContent.ProjectileType<StarspearProjectileThrown>();
				item.shootSpeed = 16f;
				item.useStyle = ItemUseStyleID.SwingThrow;
			}
			else
            {
				item.useTime = 24;
				item.useAnimation = 18;
				item.shoot = ModContent.ProjectileType<StarspearProjectile>();
				item.shootSpeed = 4.5f;
				item.useStyle = ItemUseStyleID.HoldingOut;

				return player.ownedProjectileCounts[item.shoot] < 1;
			}

			return true;
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if (EternalPlayer.StarbornArmorMeleeBonus)
            {
				if (player.altFunctionUse == 2)
                {
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
					for (int i = 0; i < 3; i++)
						Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
				else
                {
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
				}

			}
            return true;
        }

    }
}
