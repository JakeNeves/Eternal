using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Eternal.Content.Projectiles.Bobbers;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Tools
{
    public class CometiteWhaleHarvester : ModItem
    {
		public override void SetStaticDefaults()
		{
			/* Tooltip.SetDefault("Fires multiple lines at once." +
							 "\nCan fish in lava." +
							 "\nThe fishing line never snaps." +
							 "\n'Just don't use truffle worms with this, or even bloodworms...'"); */

			ItemID.Sets.CanFishInLava[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.GoldenFishingRod);

			Item.rare = ModContent.RarityType<Teal>();
			Item.fishingPole = 750;
			Item.shootSpeed = 18f;
			Item.shoot = ModContent.ProjectileType<CometiteWhaleHarvesterBobber>();
		}

		public override void HoldItem(Player player)
		{
			player.accFishingLine = true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int bobberAmount = 5 + Main.rand.Next(7);
			float spreadAmount = 80f;

			for (int index = 0; index < bobberAmount; ++index)
			{
				Vector2 bobberSpeed = velocity + new Vector2(Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f, Main.rand.NextFloat(-spreadAmount, spreadAmount) * 0.05f);

				Projectile.NewProjectile(source, position, bobberSpeed, type, 0, 0f, player.whoAmI);
			}
			return false;
		}
	}
}
