using Eternal.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Radiant
{
    public class RadiantTestWeapon : RadiantItem
    {
        public override string Texture => "Terraria/Item_" + ItemID.PulseBow;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("For Testing Purposes");
        }

        public static void AddHacks()
        {
            On.Terraria.Player.GetWeaponDamage += PlayerOnGetWeaponDamage;
			On.Terraria.Player.GetWeaponKnockback += PlayerOnGetWeaponKnockback;
        }

		private static float PlayerOnGetWeaponKnockback(On.Terraria.Player.orig_GetWeaponKnockback orig, Player self, Item sitem, float knockback)
		{
			bool isRTestWeapon = sitem.type == ModContent.ItemType<RadiantTestWeapon>();
			if (isRTestWeapon) sitem.ranged = true;

			float kb = orig(self, sitem, knockback);
			if (isRTestWeapon) sitem.ranged = false;
			return kb;
		}

		private static int PlayerOnGetWeaponDamage(On.Terraria.Player.orig_GetWeaponDamage orig, Player self, Item sitem)
		{
			bool isRTestWeapon = sitem.type == ModContent.ItemType<RadiantTestWeapon>();
			if (isRTestWeapon) sitem.ranged = true;

			int dmg = orig(self, sitem);
			if (isRTestWeapon) sitem.ranged = false;
			return dmg;
		}

		public override void SafeSetDefaults()
		{
			item.CloneDefaults(ItemID.PulseBow);
			item.Size = new Vector2(28, 46);
			item.damage = 1000;
			item.crit = 15;
			item.knockBack = 1.75f;
			item.rare = ItemRarityID.Red;

			etherealPowerCost = 1;
		}

		public override void GetWeaponCrit(Player player, ref int crit)
		{
			crit = Main.LocalPlayer.rangedCrit - Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].crit + Main.HoverItem.crit;
			base.GetWeaponCrit(player, ref crit);
		}
	}
}
