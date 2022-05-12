using Eternal.Items.Materials;
using Eternal.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Hell
{
    public class TheCleaver : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Cleaver");
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\n'One swing with this, you can easily make prime cuts!'\nHell");
        }

        public override void SetDefaults()
        {
            item.width = 40;
            item.height = 12;
            item.damage = 110;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2.3f;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(gold: 8);
            item.useTime = 15;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item1;
            item.shoot = ModContent.ProjectileType<TheCleaverProjectile>();
            item.autoReuse = true;
            item.shootSpeed = 16.5f;
            item.noUseGraphic = true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Hell;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<KnifeHandle>());
            recipe.AddIngredient(ModContent.ItemType<CleaverHead>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
