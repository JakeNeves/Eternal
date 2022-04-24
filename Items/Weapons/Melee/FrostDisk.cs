using Eternal.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class FrostDisk : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Feel the frostburn'");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 12;
            item.useAnimation = 12;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 20f;
            item.value = Item.buyPrice(gold: 30, silver: 95);
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 8.2f;
            item.shoot = ProjectileType<FrostDiskProjectile>();
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

    }
}
