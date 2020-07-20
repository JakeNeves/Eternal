using System;
using Eternal.Projectiles;
using Eternal.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons
{
    class HellHacker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws a Double-Sided Sythe\n<right> to use like a Thorium Sythe");
        }
        public override void SetDefaults()
        {
            item.damage = 275;
            item.melee = true;
            item.width = 86;
            item.height = 78;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 25;
            item.value = Item.buyPrice(gold: 30, silver: 95);
            item.rare = ItemRarityID.Red;
            item.shootSpeed = 5f;
            item.shoot = ProjectileType<HellHackerProjectile>();
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.channel = true;
                item.useTime = 10;
                item.useAnimation = 10;
                item.shoot = ProjectileType<HellHackerSpin>();
                item.UseSound = SoundID.Item1;
            }
            else
            {
                item.channel = false;
                item.useTime = 25;
                item.useAnimation = 25;
                item.shoot = ProjectileType<HellHackerProjectile>();
                item.UseSound = SoundID.Item1;
            }

            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true && base.CanUseItem(player);

        }

        public override bool UseItemFrame(Player player)
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AtomReconstructor>());
            recipe.AddIngredient(ItemType<AncientCrystalineScrap>(), 2);
            recipe.AddIngredient(ItemID.SpectreBar, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
    
}
