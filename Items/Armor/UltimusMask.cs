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
    public class UltimusMask : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("30% increased melee damage");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 22;
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
            player.setBonus = "42% increased melee damage and 25% increased melee speed" +
                            "\nSome melee weapons recieve special modifiers" +
                            "\nYou emit a source of light" +
                            "\nStarborn and Arkanium Armor Effects";
            player.meleeDamage += 0.42f;
            player.meleeSpeed += 0.25f;

            Lighting.AddLight(player.Center, 1.14f, 0.22f, 1.43f);

            EternalGlobalProjectile.starbornArmor = true;
            EternalPlayer.ArkaniumArmor = true;
            EternalPlayer.StarbornArmorMeleeBonus = true;
            EternalPlayer.UltimusArmor = true;
            EternalPlayer.UltimusArmorMeleeBonus = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += 0.30f;
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
            recipe.AddIngredient(ModContent.ItemType<ArkaniumMask>());
            recipe.AddIngredient(ModContent.ItemType<StarbornMask>());
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 8);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
