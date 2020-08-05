using Eternal.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons
{
    public class X43R0THPow : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("X43R0TH Pow");
            Tooltip.SetDefault("'The mech dragon's wrecking ball'");
        }

        public override void SetDefaults()
        {
			item.width = 16;
			item.height = 18;
			item.value = Item.sellPrice(platinum: 5);
			item.rare = ItemRarityID.Red;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.useAnimation = 40;
			item.useTime = 40;
			item.knockBack = 8f;
			item.damage = 4600;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<X43R0THPowProjectile>();
			item.shootSpeed = 15.5f;
			item.UseSound = SoundID.Item1;
			item.melee = true;
			item.crit = 5;
			item.channel = true;
		}

    }
}
