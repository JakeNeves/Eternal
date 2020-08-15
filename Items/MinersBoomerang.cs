using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items
{
    class MinersBoomerang : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner's Boomerang");
            Tooltip.SetDefault("Capible of mining blocks");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 32;
            item.damage = 30;
            item.pick = 200;
            item.noUseGraphic = true;
            item.melee = true;
            item.value = Item.sellPrice(gold: 3, silver: 30);
            item.rare = 4;
            item.shootSpeed = 15f;
            item.shoot = mod.ProjectileType("MinersBoomerangProjectile");
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.maxStack = 2;
            item.useStyle = 1;
            item.knockBack = 25;
            item.useTime = 25;
            item.useAnimation = 25;
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
