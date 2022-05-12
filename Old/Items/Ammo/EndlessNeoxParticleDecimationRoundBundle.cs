using Eternal.Projectiles.Weapons.Ranged;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Ammo
{
    public class EndlessNeoxParticleDecimationRoundBundle : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endless N30X Particle Decimation Round Bundle");
            Tooltip.SetDefault("Unlimited Ammo\nFires an ultra-fast bullet that pierces enemies");
        }

        public override void SetDefaults()
        {
            item.damage = 110;
            item.ranged = true;
            item.width = 28;
            item.height = 34;
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
            recipe.AddTile(TileID.CrystalBall);
            recipe.AddIngredient(ModContent.ItemType<NeoxParticleDecimationRound>(), 3996);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
