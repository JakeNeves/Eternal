using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class ExosiivaGladiusBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires Multiple Exosiiva Swords" +
                             "\n'It's a Literal Bullet Hell'" +
                             "\n[c/FC036B:Developer Item]" +
                             "\nDedicated to [c/038CFC:JakeTEM]");
        }

        public override void SetDefaults()
        {
            item.width = 102;
            item.height = 102;
            item.damage = 20000;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 10;
            item.rare = ItemRarityID.Purple;
            item.value = Item.sellPrice(platinum: 5, gold: 50);
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<ExosiivaProjectile>();
            item.shootSpeed = 12.25f;
            item.useTime = 18;
            item.useAnimation = 18;
            item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<StellarAlloy>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ModContent.ItemType<SiivaniteRiftBlade>());
            recipe.AddIngredient(ModContent.ItemType<SiivaniteAlloy>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 16);
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
            int numberProjectiles = Main.rand.Next(2, 4);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                Main.PlaySound(SoundID.Item8, Main.myPlayer);
            }
            int spread = 2;
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
