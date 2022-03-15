using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Materials;
using System.Collections.Generic;
using Eternal.Tiles;
using Eternal.Dusts;
using Microsoft.Xna.Framework;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class UltimusHood : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("30% increased ranged damage");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.value = Item.sellPrice(platinum: 6);
            item.rare = ItemRarityID.Red;
            item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UltimusPlateMail>() && legs.type == ModContent.ItemType<UltimusLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "42% increased ranged damage" +
                            "\nYou emit a source of light" +
                            "\nStarborn weapons cost 0 mana" +
                            "\nStarborn and Arkanium Armor Effects";
            player.rangedDamage += 0.42f;

            Lighting.AddLight(player.Center, 1.14f, 0.22f, 1.43f);

            EternalGlobalProjectile.starbornArmor = true;
            EternalPlayer.ArkaniumArmor = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.30f;
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
            recipe.AddIngredient(ModContent.ItemType<ArkaniumHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<StarbornHeadgear>());
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 8);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
