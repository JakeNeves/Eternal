using Eternal.Projectiles;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons
{
    class StarcrescentMoondisk : ModItem
    {

        public override void SetDefaults()
        {
            item.damage = 2000;
            item.melee = true;
            item.width = 54;
            item.height = 54;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 25;
            item.value = Item.buyPrice(gold: 30, silver: 95);
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 5.75f;
            item.shoot = ProjectileType<StarcrescentMoondiskProjectile>();
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;

        }

    }
}
