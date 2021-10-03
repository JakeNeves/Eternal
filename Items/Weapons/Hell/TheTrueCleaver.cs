using Eternal.Items.Materials;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using System.Collections.Generic;
using Eternal.Projectiles.Weapons.Melee;

namespace Eternal.Items.Weapons.Hell
{
    public class TheTrueCleaver : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/FCA5033:Throwing Item]\nThrows a cleaver that explodes into ghost cleavers upon impact\n'Powered by corruption...'\nHell");
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.melee = true;
			item.noUseGraphic = true;
            item.autoReuse = true;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 14;
            item.useAnimation = 14;
            item.width = 42;
            item.height = 16;
            item.knockBack = 4.5f;
            item.rare = ItemRarityID.Red;
            item.shoot = ProjectileType<TheTrueCleaverProjectile>();
            item.shootSpeed = 24.5f;
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
            recipe.AddIngredient(ItemType<TheCleaver>());
            recipe.AddIngredient(ItemID.SoulofSight, 6);
            recipe.AddIngredient(ItemID.SoulofMight, 6);
            recipe.AddIngredient(ItemID.SoulofFright, 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
