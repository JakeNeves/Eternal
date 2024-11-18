using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Eternal.Content.Projectiles.Bobbers;
using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Tools
{
    public class StarbornWhaleHarvester : ModItem
    {
		public override void SetStaticDefaults()
		{
			ItemID.Sets.CanFishInLava[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GoldenFishingRod);

			Item.width = 46;
			Item.height = 32;
			Item.rare = ModContent.RarityType<Teal>();
			Item.fishingPole = 750;
			Item.shootSpeed = 18f;
			Item.shoot = ModContent.ProjectileType<StarbornWhaleHarvesterBobber>();
		}

        public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            lineOriginOffset = new Vector2(43, -30);

            if (bobber.ModProjectile is StarbornWhaleHarvesterBobber starbornWhaleHarvesterBobber)
            {
                lineColor = starbornWhaleHarvesterBobber.FishingLineColor;
            }
            else
            {
                lineColor = Main.DiscoColor;
            }
        }

        public override void HoldItem(Player player)
		{
			player.accFishingLine = true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int bobberAmount = 3 + Main.rand.Next(4);
			float spreadAmount = 80f;

			for (int index = 0; index < bobberAmount; ++index)
			{
				Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

				Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
			}
			return false;
		}

        public override void AddRecipes()
        {
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<CometiteBar>(), 12)
				.AddIngredient(ModContent.ItemType<StarpowerCrystal>(), 6)
				.AddTile(ModContent.TileType<Starforge>())
				.Register();
        }
    }
}
