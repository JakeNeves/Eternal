using Eternal.Items.Materials;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class StarstormAlpha : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 76;
            item.height = 76;
            item.damage = 300;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2.6f;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shoot = ProjectileID.Starfury;
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
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 4 + Main.rand.Next(4);

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                if (Main.rand.Next(4) == 0)
                {
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileID.StarWrath, damage, knockBack, player.whoAmI);
                }
                else
                {
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
            }
            return false;
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CometiteCrystal>(), 10);
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 60);
            recipe.AddIngredient(ModContent.ItemType<VividMilkyWayClimax>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
