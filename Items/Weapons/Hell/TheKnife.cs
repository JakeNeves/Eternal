using Eternal.Projectiles.Weapons.Throwing;
using Eternal.Items.Materials;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using System.Collections.Generic;

namespace Eternal.Items.Weapons.Hell
{
    public class TheKnife : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\nThrows a knife that will return to the thrower\n'stab stab stab!'\nHell");
        }

        public override void SetDefaults()
        {
            item.damage = 110;
            item.ranged = true;
			item.noUseGraphic = true;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 14;
            item.useAnimation = 14;
            item.width = 26;
            item.height = 46;
            item.knockBack = 2f;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<TheKnifeProjectile>();
            item.shootSpeed = 16.5f;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
                {
                    return false;
                }
            }
            return true;
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
            recipe.AddIngredient(ItemType<KnifeHandle>());
            recipe.AddIngredient(ItemType<KnifeBlade>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
