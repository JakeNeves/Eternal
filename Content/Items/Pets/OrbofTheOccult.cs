using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Pets
{
    public class OrbofTheOccult : ModItem
    {
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.height = 14;
			Item.rare = ItemRarityID.Master;
			Item.master = true;
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = ModContent.ProjectileType<Projectiles.Pets.OcculticConstruct>();
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 5, 50);
			Item.buffType = ModContent.BuffType<Buffs.Pets.OcculticConstruct>();
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
