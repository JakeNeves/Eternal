using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal.Items.Weapons
{
    class SevenBladeofTheSevenGod : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("7 Blade of the Seven God");
            Tooltip.SetDefault("'The Grandest Blade of Them all!'\n1992 1");
        }

        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 42;
            item.damage = 77777;
            item.knockBack = 77;
            item.value = Item.buyPrice(platinum: 7);
            item.useTime = 17;
            item.useAnimation = 17;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Red;
            item.autoReuse = true;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<StellarAlloy>(), 7);
            recipe.AddIngredient(ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemType<FrostGladiator>());
            recipe.AddIngredient(ItemType<ScorchedMetal>(), 77);
            recipe.AddIngredient(ItemType<VividMilkyWayClimax>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(777, 77, 7);
        }

    }
}
