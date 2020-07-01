using System;
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
            Tooltip.SetDefault("Onced Handcrafted by demons, only to be shattered into ancient particles, \nhas became a defensive weapon weilded by ancient demons and eldritch blademasters... \nNow it's a collectors item in the modern days as there are very few of these dual-bladed throwing weapons.");
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
            item.useStyle = 1;
            item.knockBack = 25;
            item.value = Item.buyPrice(gold: 30, silver: 95);
            item.rare = 10;
            item.shootSpeed = 36f;
            item.shoot = mod.ProjectileType("HellHackerProjectile");
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
