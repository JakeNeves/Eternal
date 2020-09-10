using Eternal.Items;
using Eternal.Projectiles;
using Eternal.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal.Items.Weapons.Melee
{
    public class TheTrinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires three yoyos at once\n'The perfect balance within all the elementals of the trio!'");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 20;
            item.height = 16;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 30f;
            item.knockBack = 5.5f;
            item.damage = 10000;
            item.rare = ItemRarityID.Red;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(platinum: 9);
            item.shoot = ProjectileType<TheTrinityProjectile>();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<StellarAlloy>(), 5);
            recipe.AddIngredient(ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemType<Pyroyo>());
            recipe.AddIngredient(ItemType<Permafrost>());
            recipe.AddIngredient(ItemType<Wasteland>());
            recipe.AddIngredient(ItemType<ThunderiteBar>(), 10);
            recipe.AddIngredient(ItemType<SydaniteBar>(), 10);
            recipe.AddIngredient(ItemType<ScoriumBar>(), 10);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(Main.DiscoR, 30, Main.DiscoB);
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool AllowPrefix(int pre)
        {
            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
            {
                return false;
            }
            return true;
        }

       public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3;
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            int spread = 5;
            float spreadMult = 0.5f;
            for (int i = 0; i < 3; i++)
            {
                float vX = speedX + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = speedY + (float)Main.rand.Next(-spread, spread + 1) * spreadMult;
                Projectile.NewProjectile(position.X, position.Y, vX, vY, type, damage, knockBack, Main.myPlayer);
            }
            return false;
        }

    }
}
