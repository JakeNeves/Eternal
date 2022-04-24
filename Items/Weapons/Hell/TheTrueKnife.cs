using Eternal.Projectiles.Weapons.Throwing;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Hell
{
    public class TheTrueKnife : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\nThrows a knife that will explode into bolts upon impact\n'Powered by corruption...'\nHell");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.ranged = true;
            item.noUseGraphic = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 14;
            item.useAnimation = 14;
            item.width = 44;
            item.height = 14;
            item.knockBack = 4.5f;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<TheTrueKnifeProjectile>();
            item.shootSpeed = 24.5f;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 500; ++i)
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
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemType<TheKnife>());
            recipe.AddIngredient(ItemID.SoulofSight, 6);
            recipe.AddIngredient(ItemID.SoulofMight, 6);
            recipe.AddIngredient(ItemID.SoulofFright, 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
