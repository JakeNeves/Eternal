using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Ammo
{
    public class StarbornArrow : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Brittle, but tough!'");
        }

        public override void SetDefaults()
        {
            item.damage = 100;
            item.ranged = true;
            item.width = 14;
            item.height = 34;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 4f;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<StarbornArrowProjectile>();
            item.shootSpeed = 20.5f;
            item.ammo = AmmoID.Arrow;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkTeal;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<Starforge>());
            recipe.AddIngredient(ItemType<CometiteBar>(), 6);
            recipe.AddIngredient(ItemType<GalaxianPlating>(), 12);
            recipe.SetResult(this, 333);
            recipe.AddRecipe();
        }

    }
}
