using Eternal.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class Frostpike : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Frostpike");
            Tooltip.SetDefault("'Penetrate your enemies with pure blue ice!'");
        }

        public override void SetDefaults()
        {
            item.damage = 110;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useAnimation = 12;
            item.useTime = 24;
            item.shootSpeed = 4.5f;
            item.knockBack = 65f;
            item.width = 40;
            item.height = 40;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 1);

            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.UseSound = SoundID.Item1;
            item.shoot = ProjectileType<FrostpikeProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
        }

    }
}
