using Eternal.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Tiles;

namespace Eternal.Items.Weapons.Melee
{
    public class ExosiivaGladiusBlade : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Multiple Exosiiva Swords\n'It's a Literal Bullet Hell'\n[c/FF0000:Warning] - May cause frame drops at the cost of multiple projectiles existing off-screen...\nHowever the frames will go back to normal eventually.\n[c/FC036B:Developer Item]\nDedicated to [c/038CFC:JakeTEM]");
        }

        public override void SetDefaults()
        {
            item.width = 104;
            item.height = 110;
            item.damage = 20000;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 10;
            item.rare = ItemRarityID.Purple;
            item.value = Item.sellPrice(platinum: 10, gold: 50);
            item.autoReuse = true;
            item.shoot = ProjectileType<ExosiivaProjectile>();
            item.shootSpeed = 9.5f;
            item.useTime = 12;
            item.useAnimation = 12;
            item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<StellarAlloy>(), 5);
            recipe.AddIngredient(ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemType<SiivaniteRiftBlade>());
            recipe.AddIngredient(ItemType<SiivaniteAlloy>(), 5);
            recipe.AddIngredient(ItemType<PrismaticFractal>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.DarkBlue;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(2, 12);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType<SiivaSpark>(), damage, knockBack, player.whoAmI);
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
            }
            int spread = 10;
            float spreadMult = 0.5f;
            for (int i = 0; i < 3; i++)
            {
                float vX = speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Main.myPlayer);
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
            }
            return false;
        }

    }
}
