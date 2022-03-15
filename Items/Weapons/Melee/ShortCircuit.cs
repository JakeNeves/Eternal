using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;

namespace Eternal.Items.Weapons.Melee
{
    public class ShortCircuit : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'You like spinning things, do you?'");
        }

        public override void SetDefaults()
        {
            item.width = 162;
            item.height = 162;
            item.damage = 1200;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.knockBack = 3.5f;
            item.shoot = ModContent.ProjectileType<ShortCircuitProjectile>();
            item.shootSpeed = 50f;
            item.UseSound = SoundID.DD2_GhastlyGlaivePierce;
            item.rare = ItemRarityID.Red;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[item.shoot] < 1;
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
            recipe.AddIngredient(ModContent.ItemType<FrightCore>(), 30);
            recipe.AddIngredient(ModContent.ItemType<SmiteCore>(), 20);
            recipe.AddIngredient(ModContent.ItemType<WeatheredPlating>(), 25);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
