using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Eternal.Tiles;
using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;

namespace Eternal.Items.Weapons.Melee
{
    public class NeoxChromaBreakerBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("N30X Chroma Breaker Blade");
            Tooltip.SetDefault("'Never Underestimate the Breaker Blade's gamer form!'");
        }


        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 54;
            item.damage = 2000;
            item.knockBack = 2f;
            item.value = Item.sellPrice(platinum: 3);
            item.useTime = 25;
            item.useAnimation = 25;
            item.UseSound = SoundID.Item71;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.shoot = ModContent.ProjectileType<NeoxChromaBreakerBladeProjectile>();
            item.shootSpeed = 20f;
            item.autoReuse = true;
            item.melee = true;
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
            recipe.AddIngredient(ModContent.ItemType<VividMilkyWayClimax>());
            recipe.AddIngredient(ItemID.BreakerBlade);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
