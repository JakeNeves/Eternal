using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Pets
{
    public class ReminantHead : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Reminant Head");
			Tooltip.SetDefault("Summons a Cosmic Reminant to emit light" +
							 "\n'yeah dude this is pretty spooky...'");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.WispinaBottle);

			Item.width = 20;
			Item.height = 18;
			Item.rare = ItemRarityID.Master;
			Item.master = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Pets.CosmicReminant>();
			Item.buffType = ModContent.BuffType<Buffs.Pets.CosmicReminant>();
		}

		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600);
			}
		}
	}
}
