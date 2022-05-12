using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Ammo
{
    public class NeoxParticleDecimationRound : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("N30X Particle Decimation Round");
            Tooltip.SetDefault("Fires an ultra-fast bullet that pierces enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 110;
            item.ranged = true;
            item.width = 14;
            item.height = 30;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 3f;
            item.rare = ItemRarityID.Red;
            item.shoot = ModContent.ProjectileType<NeoxParticleDecimatorProjectile>();
            item.shootSpeed = 25.75f;
            item.ammo = AmmoID.Bullet;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<NeoxCore>(), 3);
            recipe.AddIngredient(ModContent.ItemType<SightCore>());
            recipe.SetResult(this, 333);
            recipe.AddRecipe();
        }

    }
}
