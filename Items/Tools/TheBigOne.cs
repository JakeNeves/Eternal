using Eternal.Items.Materials;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Tools
{
    public class TheBigOne : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jaer Hend's Mighty Hamaxe of Eternity");
            Tooltip.SetDefault("A Very Big Axe\n'Sprited by Jaer Hend'\n[c/FC036B:Developer Item]\nDedicated to [c/038CFC:Jaer Hend]");
        }

        public override void SetDefaults()
        {
            item.damage = 10000;
            item.melee = true;
            item.width = 164;
            item.height = 162;
            item.useTime = 25;
            item.useAnimation = 25;
            item.axe = 100;
            item.hammer = 200;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 0;
            item.value = Item.buyPrice(platinum: 1);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<StellarAlloy>(), 5);
            recipe.AddIngredient(ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemID.PossessedHatchet);
            recipe.AddIngredient(ItemType<CoreofEternal>(), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItemFrame(Player player)
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
            return true;
        }

    }
}
