using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    public class EmperorsGift : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Emperor's Gift");
            Tooltip.SetDefault("90% increased damage" +
                               "\n+200 max life" +
                               "\n+8 max minions" +
                               "\nAllows the wearer to run at Fair Speed!" +
                               "\nAllows the wearer to run at God Speed when equipped with Cosmic Starstryder Treads!" +
                               "\n'Ascendance beyond your capabilities...'");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(8, 4));
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.accessory = true;
            item.value = Item.sellPrice(platinum: 15);
            item.rare = ItemRarityID.Red;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            EternalGlobalProjectile.emperorsGift = true;

            player.allDamage += 0.9f;
            player.maxMinions += 8;
            player.statLifeMax2 += 200;
            player.accRunSpeed += 4.8f;
            player.moveSpeed += 0.15f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
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
            recipe.AddIngredient(ItemType<StellarAlloy>(), 4);
            recipe.AddIngredient(ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemType<CometiteBar>(), 40);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
