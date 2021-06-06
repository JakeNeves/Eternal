using Eternal.Items;
using Eternal.Projectiles.Weapons.Melee;
using Eternal.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Eternal.Items.Materials;
using Terraria.DataStructures;

namespace Eternal.Items.Weapons.Melee
{
    public class TheTrinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires three yoyos at once" +
                "\n'The perfect balance within all the elementals of the trio!'" +
                "\n[c/FF0000:Warning] - This could cause frame drops and possible lag, use carefully!");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 20;
            item.height = 54;
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
            item.shoot = ProjectileType<TheTrinityBaseProjectile>();
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
            tooltips[0].overrideColor = new Color(Main.DiscoR, 255, 0);
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
    }
}