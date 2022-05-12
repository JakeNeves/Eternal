using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class StarstormOmega : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/ff0000:Warning]" +
                             "\nThis weapon can cause lag, use at your own risk!" +
                             "\n'Where do we even go from here?'");
        }

        public override void SetDefaults()
        {
            item.width = 130;
            item.height = 130;
            item.damage = 2600;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2.6f;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shoot = ModContent.ProjectileType<Projectiles.Weapons.Melee.StarstormOmega>();
            item.shootSpeed = 12f;
            item.UseSound = SoundID.Item1;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
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

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 8 + Main.rand.Next(8);

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(Main.rand.Next(16, 30)));
                if (Main.rand.Next(2) == 0)
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<StarstormSwordOmega2>(), damage, knockBack, player.whoAmI);
                else
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                Main.PlaySound(SoundID.DD2_FlameburstTowerShot, Main.myPlayer);
            }
            for (int index = 0; index < numberProjectiles; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 0.5 + Main.rand.Next(200) * -player.direction + (Main.mouseX + (double)Main.screenPosition.X - player.position.X)), (float)(player.position.Y + player.height * 0.5 - 600.0));
                vector2_1.X = (float)((vector2_1.X + (double)player.Center.X) / 2.0) + Main.rand.Next(-150, 200);
                vector2_1.Y -= 100 * index;
                float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if (num13 < 0.0)
                {
                    num13 *= -1f;
                }

                if (num13 < 20.0)
                {
                    num13 = 20f;
                }

                float num14 = (float)Math.Sqrt(num12 * (double)num12 + num13 * (double)num13);
                float num15 = item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + Main.rand.Next(-4, 6) * 0.02f;
                float SpeedY = num17 + Main.rand.Next(-4, 6) * 0.02f;
                Projectile.NewProjectile(vector2_1.X, vector2_1.Y, SpeedX, SpeedY, ModContent.ProjectileType<StarstormSwordOmega>(), damage, knockBack, Main.myPlayer, 0.0f, Main.rand.Next(5));
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<NeoxCore>(), 18);
            recipe.AddIngredient(ModContent.ItemType<SiivaniteAlloy>(), 12);
            recipe.AddIngredient(ModContent.ItemType<InterstellarSingularity>(), 16);
            recipe.AddIngredient(ModContent.ItemType<StarstormGamma>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
