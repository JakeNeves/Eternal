using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Pets
{
    public class ArkrystalEmerald : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arkrystal Emerald");
			Tooltip.SetDefault("Summons an Ark that is loayal and follows you" +
							 "\n'Is it a weapon or a pet? Well, doesn't matter...'");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);

			Item.width = 30;
			Item.height = 26;
			Item.rare = ItemRarityID.Master;
			Item.master = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.Pets.Ark>();
			Item.buffType = ModContent.BuffType<Buffs.Pets.Ark>();
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
